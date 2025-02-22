using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> coinTextFields;

    private void Start()
    {
       
        CoinSploinkerController.Instance.UpdateUI();
    }
    public void UpdateCoinDisplays(int coins)
    {
        foreach (var textField in coinTextFields)
        {
            if (textField != null)
            {
                textField.text = coins.ToString();
            }
        }
    }
}
