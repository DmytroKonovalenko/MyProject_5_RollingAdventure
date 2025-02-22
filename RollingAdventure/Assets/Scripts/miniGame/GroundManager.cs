using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundManager : MonoBehaviour
{

    public GameObject firstGround;
    public GameObject groundPrefab;
    public GameObject parentObject;
    public UIManager_tetromino UIManager;
    public CameraController_tetromino cameraController;
    public bool finishMoveGround = false;
    public int numberOfGround = 5;

    private float timeToDestroyGround = 0.15f;
    private List<GameObject> listGroundForWard = new List<GameObject>();
    private List<GameObject> listGroundBack = new List<GameObject>();
    private Vector3 firstForwardPosition;
    private Vector3 firstBackPosition;
    private bool finishedRandomGround;
    private bool enableCheck = true;
    private bool stopMoveGround = false;
    private bool finishedScaleGroundWhenGameOver = false;
    private float timeToMove = 1f;

    void Start()
    {
        firstForwardPosition = firstGround.transform.position + Vector3.forward * firstGround.transform.localScale.z + new Vector3(0, -10f, 0);
        firstBackPosition = firstGround.transform.position + Vector3.back * firstGround.transform.localScale.z + new Vector3(0, -10f, 0);
        StartCoroutine(RandomGroundOnForward(firstForwardPosition, numberOfGround, listGroundForWard));
        StartCoroutine(RandomGroundOnBack(firstBackPosition, numberOfGround, listGroundBack));
    }

    void Update()
    {
        if (finishedRandomGround && !stopMoveGround)
        {
            foreach (var ground in listGroundForWard)
                StartCoroutine(MoveGround(ground, ground.transform.position, ground.transform.position + new Vector3(0, 10f, 0), timeToMove));
            foreach (var ground in listGroundBack)
                StartCoroutine(MoveGround(ground, ground.transform.position, ground.transform.position + new Vector3(0, 10f, 0), timeToMove));
            stopMoveGround = true;
        }

        if (PlayerController.gameOver && !finishedScaleGroundWhenGameOver)
        {
            finishedScaleGroundWhenGameOver = true;
            StartCoroutine(ScaleGroundSmaller(firstGround, 0));
            foreach (var ground in new List<GameObject>(listGroundForWard))
                StartCoroutine(ScaleGroundSmaller(ground, timeToDestroyGround * listGroundForWard.IndexOf(ground)));
            listGroundForWard.Clear();
            foreach (var ground in new List<GameObject>(listGroundBack))
                StartCoroutine(ScaleGroundSmaller(ground, timeToDestroyGround * listGroundBack.IndexOf(ground)));
            listGroundBack.Clear();
        }

        if (ScoreManager_tetromino.Instance.Score != 0 && ScoreManager_tetromino.Instance.Score % UIManager.scoreToScaleGround == 0 && enableCheck && !PlayerController.gameOver)
        {
            enableCheck = false;
            StartCoroutine(ScaleGroundSmaller(listGroundForWard[^1], 0f));
            StartCoroutine(ScaleGroundSmaller(listGroundBack[^1], 0f));
            StartCoroutine(WaitAndEnableCheck());
        }

        if (cameraController.startToRotateCamera && enableCheck)
        {
            enableCheck = false;
            StartCoroutine(ScaleGroundBigger(listGroundForWard[^1], 0f));
            StartCoroutine(ScaleGroundBigger(listGroundBack[^1], 0f));
            StartCoroutine(WaitAndEnableCheck());
        }
    }

    IEnumerator RandomGroundOnForward(Vector3 position, int number, List<GameObject> newList)
    {
        finishedRandomGround = false;
        for (int i = 0; i < number; i++)
        {
            var currentGround = Instantiate(groundPrefab, position, Quaternion.identity);
            newList.Add(currentGround);
            currentGround.transform.SetParent(parentObject.transform);
            position = currentGround.transform.position + Vector3.forward * currentGround.transform.localScale.z;
            yield return new WaitForSeconds(0.1f);
        }
        finishedRandomGround = true;
    }

    IEnumerator RandomGroundOnBack(Vector3 position, int number, List<GameObject> newList)
    {
        for (int i = 0; i < number; i++)
        {
            var currentGround = Instantiate(groundPrefab, position, Quaternion.identity);
            newList.Add(currentGround);
            currentGround.transform.SetParent(parentObject.transform);
            position = currentGround.transform.position + Vector3.back * currentGround.transform.localScale.z;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MoveGround(GameObject ground, Vector3 startpos, Vector3 endPos, float timeToMove)
    {
        float t = 0;
        while (t < timeToMove)
        {
            ground.transform.position = Vector3.Lerp(startpos, endPos, t / timeToMove);
            t += Time.deltaTime;
            yield return null;
        }
        ground.transform.position = endPos;
        finishMoveGround = true;
    }

    IEnumerator ScaleGroundSmaller(GameObject ground, float time)
    {
        yield return new WaitForSeconds(time);
        ground.GetComponent<Animator>().Play("ScaleSmaller");
    }

    IEnumerator ScaleGroundBigger(GameObject ground, float time)
    {
        yield return new WaitForSeconds(time);
        ground.GetComponent<MeshRenderer>().enabled = true;
        ground.GetComponent<Animator>().Play("ScaleBigger");
    }

    IEnumerator WaitAndEnableCheck()
    {
        yield return new WaitForSeconds(3f);
        enableCheck = true;
    }
}