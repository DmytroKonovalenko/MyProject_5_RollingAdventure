using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager_tetromino : MonoBehaviour {
    public GroundManager groundManager;
    public CameraController_tetromino cameraController;
    public Text score;
    public Text bestScore;
   
    public Image replayButton;
    public int scoreToScaleGroundIncrease = 15; 
    public int scoreToRotateCameraIncrease = 20;


   
    public int scoreToScaleGround = 15; 
    public int scoreToRotateCamera = 20;



    private bool enableCheck = true;
    void Start () {
        ScoreManager_tetromino.Instance.Reset();
       
        replayButton.enabled = false;
        StartCoroutine(CountScore());
	}
	

	void Update () {
        score.text = ScoreManager_tetromino.Instance.Score.ToString();
        bestScore.text = ScoreManager_tetromino.Instance.HighScore.ToString();      
        if (cameraController.startToRotateCamera && enableCheck) 
        {
            enableCheck = false;
            scoreToScaleGround += scoreToScaleGroundIncrease;
            scoreToRotateCamera += scoreToRotateCameraIncrease;
            StartCoroutine(WaitAndEnableCheck());
        }

        if (PlayerController.gameOver)
        {
            Invoke("EnableButton", 1.5f);
        }
	}

    IEnumerator CountScore()
    {
        while (true)
        {
            if (groundManager.finishMoveGround && !PlayerController.gameOver && !cameraController.startToRotateCamera)
            {
                ScoreManager_tetromino.Instance.AddScore(1);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator WaitAndEnableCheck()
    {
        yield return new WaitForSeconds(2f);
    }

   

    void EnableButton()
    {
        replayButton.enabled = true;
       
    }

    public void ReplayButton()
    {       
        SceneManager.LoadSceneAsync(1 , LoadSceneMode.Single);  
    }
    public void HomeButton()
    {      
        SceneManager.LoadSceneAsync(0 , LoadSceneMode.Single);  
    }
}
