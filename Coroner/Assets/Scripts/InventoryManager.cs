using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private List<string> collectedItems = new List<string>();
    public string currentItem;

    [Header("UI")]
    public Transform inventoryPanel;      
    public GameObject inventoryItemPrefab; 

    private List<string> items = new List<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        UpdateUI();
        Debug.Log(itemName + " toegevoegd aan inventaris.");
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void UseItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
            UpdateUI();
            Debug.Log(itemName + " gebruikt!");
        }
    }

    private void UpdateUI()
    {
        if (inventoryPanel == null || inventoryItemPrefab == null) return;

        
        foreach (Transform child in inventoryPanel)
            Destroy(child.gameObject);

        
        foreach (string item in items)
        {
            GameObject entry = Instantiate(inventoryItemPrefab, inventoryPanel);
            var view = entry.GetComponent<InventoryItemView>();
            if (view != null)
                view.Bind(item);
        }
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
