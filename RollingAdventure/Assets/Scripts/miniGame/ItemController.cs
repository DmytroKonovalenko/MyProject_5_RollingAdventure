﻿using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

    private Vector3 dir;
    private GameObject temp;
    // Use this for initialization
    void Start () {
        dir = Vector3.left;
        temp = GameObject.FindGameObjectsWithTag("ObstacleManager")[0];
        StartCoroutine(MoveObstacle());
    }
	
	// Update is called once per frame
	void Update () {

        if (PlayerController.gameOver)
        {
            temp.GetComponent<ObstaclesManager_tetromino>().speedItemMoving = 20f;
        }

        if (!CameraController_tetromino.isCameraRotateFinish)
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        StopCoroutine(MoveObstacle());
        Destroy(gameObject);
    }

    IEnumerator MoveObstacle()
    {
        while (true)
        {
            if (CameraController_tetromino.isCameraRotateFinish)
            {
                gameObject.transform.position = gameObject.transform.position + dir * temp.GetComponent<ObstaclesManager_tetromino>().speedItemMoving * Time.deltaTime;
            }
            yield return null;
        }
    }
}
