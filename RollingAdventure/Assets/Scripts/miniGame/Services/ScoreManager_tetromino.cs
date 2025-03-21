﻿using UnityEngine;
using System;
using System.Collections;

public class ScoreManager_tetromino : MonoBehaviour
{
    public static ScoreManager_tetromino Instance;

	public int Score { get; private set; }
	public int HighScore { get; private set; }
	public bool HasNewHighScore { get; private set; }

	public static event Action<int> ScoreUpdated = delegate {};
    public static event Action<int> HighscoreUpdated = delegate {};
	
	private const string HIGHSCORE = "HIGHSCORE_tetromino";	// key name to store high score in PlayerPrefs

    void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Reset();
    }

	public void Reset()
	{
		// Initialize score
		Score = 0;

		// Initialize highscore
		HighScore = PlayerPrefs.GetInt(HIGHSCORE, 0);
		HasNewHighScore = false;
	}

	public void AddScore(int amount)
	{
		Score += amount;

        // Fire event
        ScoreUpdated(Score);

        // Update highscore if player has made a new one
        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt(HIGHSCORE, HighScore);
            HasNewHighScore = true;

            HighscoreUpdated(HighScore);
        }
	}
}
