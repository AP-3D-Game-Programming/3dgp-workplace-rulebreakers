
using UnityEngine;
[System.Serializable]
public class ToolTestEntry
{
    public string itemName;
    public GameObject toolPrefab;
}
public class TestScript : MonoBehaviour
{
    public ToolTestEntry[] toolPrefabs;
}
