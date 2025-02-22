using UnityEngine;

public class SkinApplier : MonoBehaviour
{
    public Renderer targetRenderer;
    public SkinMaterialDatabase materialDatabase;

    private void Start()
    {
        ApplyEquippedSkin();
    }

    private void ApplyEquippedSkin()
    {
        string equippedMaterialName = PlayerPrefs.GetString("EquippedSkinMaterial", "");

        if (!string.IsNullOrEmpty(equippedMaterialName))
        {
            Material equippedMaterial = materialDatabase.GetMaterialByName(equippedMaterialName);
            if (equippedMaterial != null && targetRenderer != null)
            {
                targetRenderer.material = equippedMaterial;
            }
        }
    }
}
