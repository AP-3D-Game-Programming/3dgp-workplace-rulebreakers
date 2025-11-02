using UnityEngine;

[System.Serializable]
public class ToolEntry
{
    public string itemName;
    public GameObject toolPrefab;
}

public class ToolDatabase : MonoBehaviour
{
    public ToolEntry[] toolPrefabs;

    public GameObject GetPrefabForItem(string itemName)
    {
        if (toolPrefabs == null) return null;
        foreach (var entry in toolPrefabs)
        {
            if (entry != null && entry.itemName == itemName)
            {
                return entry.toolPrefab;
            }
        }
        Debug.LogWarning($"[ToolDatabase] Geen prefab gevonden voor item '{itemName}'.");
        return null;
    }
}
