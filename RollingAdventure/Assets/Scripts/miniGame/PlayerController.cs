using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GroundManager groundManager;
    public CameraController_tetromino cameraController;
    public ParticleSystem particle;
    public bool touchDisable;
    public float movingSpeedOfPlayer = 7;
    public float movingSpeedIncreaseOfPlayer = 0.7f;
    public float speedPlayerFalling = 20f;
    public static bool gameOver;

    private Vector3 dir;
    private float dirTurn;
    private bool enableCheck = true;
    private bool first = true;


    [Header("CoinsController")]
    private int coins;
    public TextMeshProUGUI coinsText;

    void Start()
    {
       
        gameOver = false;
        touchDisable = false;
        dirTurn = 1;
        StartCoroutine(MovePlayer());
    }

    public void UpdateCoin()
    {
        coins = 0;
        coinsText.text = coins.ToString();
    }
    public void GoHome()
    {
        SceneManager.LoadScene(0);  
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && groundManager.finishMoveGround && !touchDisable)
        {
            dirTurn = dirTurn * (-1);
            dir = dirTurn < 0 ? Vector3.forward : Vector3.back;
        }

        RaycastHit hit;
        Ray rayDown = new Ray(player.transform.position, Vector3.down);
        if (!Physics.Raycast(rayDown, out hit, 0.5f))
        {
            gameOver = true;
            touchDisable = true;
            player.GetComponent<Rigidbody>().isKinematic = false;
            movingSpeedOfPlayer = speedPlayerFalling;
            dir = Vector3.down;
            first = false;
        }

        if (cameraController.startToRotateCamera && enableCheck)
        {
            enableCheck = false;
            movingSpeedOfPlayer += movingSpeedIncreaseOfPlayer;
            StartCoroutine(WaitAndEnableCheck(2f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            touchDisable = true;
            gameOver = true;
            dir = Vector3.left;
        }

        if (other.tag == "Item")
        {
            //CoinManager.Instance.AddCoins(1);
            CoinSploinkerController.Instance.AddCoins(5);
            coins +=5;
            coinsText.text=coins.ToString();
            ParticleSystem particleTemp = Instantiate(particle, other.gameObject.transform.position, Quaternion.identity);
            particleTemp.Simulate(0.5f, true, false);
            particleTemp.Play();
            Destroy(particleTemp, 0.5f);
            Destroy(other.gameObject);
        }
    }

    IEnumerator MovePlayer()
    {
        while (true)
        {
            if (!cameraController.startToRotateCamera)
            {
                player.transform.position += dir * movingSpeedOfPlayer * Time.deltaTime;
            }
            yield return null;
        }
    }

    IEnumerator WaitAndEnableCheck(float time)
    {
        yield return new WaitForSeconds(time);
        enableCheck = true;
    }
}
