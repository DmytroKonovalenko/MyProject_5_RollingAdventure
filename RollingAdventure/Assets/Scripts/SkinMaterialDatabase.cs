using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkinMaterialDatabase", menuName = "SkinSystem/SkinMaterialDatabase")]
public class SkinMaterialDatabase : ScriptableObject
{
    public List<Material> skinMaterials;

    public Material GetMaterialByName(string materialName)
    {
        return skinMaterials.Find(m => m.name == materialName);
    }
}
