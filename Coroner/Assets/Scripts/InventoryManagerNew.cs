using UnityEngine;

[System.Serializable]
public class ToolEntry
{
    public string itemName;
    public GameObject toolPrefab;
}

public class InventoryManagerNew : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public static InventoryManagerNew Instance;
    public ItemSlot[] itemSlot;
    public HintSlot[] hintSlot;
    public ScriptObjItem[] scriptObjItems;

    [SerializeField]
    private ToolDisplayManager toolDisplayManager;

    [SerializeField]
    private PickUpSlot pickUpSlot;

    public ToolEntry[] toolPrefabs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Debug.Log("Inventory button pressed");
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);
            Time.timeScale = menuActivated ? 0 : 1;

            if (menuActivated)
            {
                if (pickUpSlot.IsSlotInUse())
                {
                    toolDisplayManager.ShowTool(pickUpSlot.GetToolPrefab());
                }
            }
            else
            {
                toolDisplayManager.HideTool();
            }
        }
    }

    public void UseItem(string itemName)
    {
        for (int i = 0; i < scriptObjItems.Length; i++)
        {
            if (scriptObjItems[i].itemName == itemName)
            {
                scriptObjItems[i].UseItem();
                Debug.Log(itemName + " ready to use");
            }
        }
    }

    public void AddItem(string itemName, Sprite inventoryIcon, string itemDescription, ItemType itemType)
    {
        if (itemType == ItemType.tool)
        {
            Debug.Log("Tool = " + itemName);
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false)
                {
                    itemSlot[i].AddItem(itemName, inventoryIcon, itemDescription, itemType);
                    return;
                }
            }
        }
        else
        {
            Debug.Log("Hint = " + itemName);
            for (int i = 0; i < hintSlot.Length; i++)
            {
                if (hintSlot[i].isFull == false)
                {
                    hintSlot[i].AddItem(itemName, inventoryIcon, itemDescription, itemType);
                    return;
                }
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }

        for (int i = 0; i < hintSlot.Length; i++)
        {
            hintSlot[i].selectedShader.SetActive(false);
            hintSlot[i].thisItemSelected = false;
        }

    }

    public ItemSlot FindEmptySlot()
    {
        foreach (ItemSlot slot in itemSlot)
        {
            if (!slot.isFull)
            {
                return slot;
            }
        }
        return null;
    }

    public GameObject GetPrefabForItem(string itemName)
    {
        foreach (ToolEntry entry in toolPrefabs)
        {
            if (entry.itemName == itemName)
                return entry.toolPrefab;
        }
        return null;
    }
}

public enum ItemType
{
    hint,
    tool
};
