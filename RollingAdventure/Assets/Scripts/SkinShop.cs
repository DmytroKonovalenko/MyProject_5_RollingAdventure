using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkinShop : MonoBehaviour
{
    public List<Button> skinButtons;
    public List<SkinObject> skins;
    private CoinSploinkerController coinSploinkerController;

    private void Start()
    {
        LoadPurchasedSkins();
        if (PlayerPrefs.GetInt("EquippedSkin", -1) == -1)
        {
            EquipSkin(0);
        }
        coinSploinkerController=FindObjectOfType<CoinSploinkerController>();
        InitializeSkinButtons();
    }

    private void InitializeSkinButtons()
    {
        for (int i = 0; i < skinButtons.Count; i++)
        {
            int index = i;
            skinButtons[i].onClick.RemoveAllListeners();
            skinButtons[i].onClick.AddListener(() => OnSkinButtonClick(index));
        }
    }

    private void OnSkinButtonClick(int index)
    {
        if (skins[index].isPurchased)
        {
            EquipSkin(index);
        }
        else
        {
            PurchaseSkin(index);
        }
    }

    private void PurchaseSkin(int index)
    {
        int price = skins[index].price;

        if (CanAfford(price))
        {
            coinSploinkerController.SpendCoins(price);
            skins[index].isPurchased = true;
            PlayerPrefs.SetInt("Skin_" + index, 1);
            PlayerPrefs.Save();
            UpdateSkinButtonState(index); 
        }
    }

    private void EquipSkin(int index)
    {
        for (int i = 0; i < skins.Count; i++)
        {
            skins[i].isEquipped = false;
            skins[i].equippedObject.SetActive(false);
            skins[i].notEquippedObject.SetActive(true);
        }

        skins[index].isEquipped = true;
        PlayerPrefs.SetInt("EquippedSkin", index);

        if (skins[index].skinMaterial != null)
        {
            PlayerPrefs.SetString("EquippedSkinMaterial", skins[index].skinMaterial.name);
        }
        else
        {
            PlayerPrefs.DeleteKey("EquippedSkinMaterial"); 
        }

        PlayerPrefs.Save();
        UpdateSkinButtonState(index);
    }

    private void UpdateSkinButtonState(int index)
    {
        skins[index].purchasedObject.SetActive(skins[index].isPurchased);
        skins[index].unPurchasedObject.SetActive(!skins[index].isPurchased);
        skins[index].equippedObject.SetActive(skins[index].isEquipped);
        skins[index].notEquippedObject.SetActive(!skins[index].isEquipped);
    }

    private void LoadPurchasedSkins()
    {
        for (int i = 0; i < skins.Count; i++)
        {
            skins[i].isPurchased = PlayerPrefs.GetInt("Skin_" + i, 0) == 1;
            skins[i].isEquipped = false;
            UpdateSkinButtonState(i); 
        }

        int equippedSkinIndex = PlayerPrefs.GetInt("EquippedSkin", -1);
        if (equippedSkinIndex >= 0 && equippedSkinIndex < skins.Count)
        {
            EquipSkin(equippedSkinIndex);
        }
    }

   

    private bool CanAfford(int price)
    {
        int playerCoins = coinSploinkerController.GetCoins();
        return playerCoins >= price;
    }
}

[System.Serializable]
public class SkinObject
{
    public string skinName;
    public int price;
    public bool isPurchased = false;
    public bool isEquipped = false;
    public GameObject purchasedObject;
    public GameObject unPurchasedObject;
    public GameObject equippedObject;
    public GameObject notEquippedObject;

    public Material skinMaterial; 
}
