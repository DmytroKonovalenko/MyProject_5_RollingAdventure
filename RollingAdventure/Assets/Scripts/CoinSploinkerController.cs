using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CoinSploinkerController : MonoBehaviour
{
    public static CoinSploinkerController Instance { get; private set; }

    private int coins;
    private const string COIN_KEY = "SavedCoins";
    private CurrencyUI currencyUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadCoins();
        FindCurrencyUI();
        UpdateUI();
    }
    private void Start()
    {
        LoadCoins();
       
        FindCurrencyUI();
        UpdateUI();
    }

    private void FindCurrencyUI()
    {
        GameObject uiObject = GameObject.Find("1otHRJJkGIDLxZpHTmiA");
        if (uiObject != null)
        {
            currencyUI = uiObject.GetComponent<CurrencyUI>();
        }
        else
        {
            Debug.LogError("CurrencyUI не знайдено в сцені! Переконайтеся, що об'єкт з такою назвою існує.");
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        SaveCoins();
      //  UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            SaveCoins();
            UpdateUI();
            return true;
        }
        return false;
    }

    public int GetCoins()
    {
        return coins;
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(COIN_KEY, coins);
        PlayerPrefs.Save();
    }

    public void LoadCoins()
    {
        coins = PlayerPrefs.HasKey(COIN_KEY) ? PlayerPrefs.GetInt(COIN_KEY) : 0;
       
    }

   public void UpdateUI()
    {
        if (currencyUI == null)
        {
            FindCurrencyUI();
        }
        if (currencyUI != null)
        {
            currencyUI.UpdateCoinDisplays(coins);
        }
    } 
    private void OnApplicationQuit()
    {
        SaveCoins();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveCoins();
        }
    }
}
