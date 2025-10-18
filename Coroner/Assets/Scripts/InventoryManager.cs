using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private List<string> collectedItems = new List<string>();
    public string currentItem;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(string itemName)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
        }
    }

    public bool HasItem(string itemName)
    {
        return collectedItems.Contains(itemName);
    }

    public void ChangeItem(string itemName)
    {
        if (collectedItems.Contains(itemName))
        {
            currentItem = itemName;
        }

        Debug.Log($"Now holding: {currentItem}");
    }
}
