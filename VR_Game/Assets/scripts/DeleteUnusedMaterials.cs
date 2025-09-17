using UnityEditor;
using UnityEngine;

public class DeleteUnusedMaterials
{
    [MenuItem("Tools/Delete Unused Materials")]
    static void DeleteUnused()
    {
        string[] allMaterials = AssetDatabase.FindAssets("t:Material");
        int deletedCount = 0;

        foreach (string guid in allMaterials)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            string[] refs = AssetDatabase.GetDependencies(path, false);

            if (refs.Length <= 1) // Only itself
            {
                AssetDatabase.DeleteAsset(path);
                deletedCount++;
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("Deleted " + deletedCount + " unused materials.");
    }
}
