using System.Net.Sockets;
using UnityEngine;

public class InventoryManagerNew : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public static InventoryManagerNew Instance;
    public ItemSlot[] itemSlot;
    public HintSlot[] hintSlot;
    public ScriptObjItem[] scriptObjItems;

    // M => door Mohamed toegevoegd
    public MouseMovement msMovement;

    [SerializeField]
    private ToolDisplayManager toolDisplayManager;

    [SerializeField]
    private PickUpSlot pickUpSlot;

    [SerializeField]
    private ToolDatabase toolDatabase;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Debug.Log("Inventory button pressed");
            // M
            msMovement.locked = !msMovement.locked;
            Cursor.lockState = msMovement.locked ? CursorLockMode.Locked : CursorLockMode.None;
            //

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
                Debug.Log("[InventoryManagerNew] Inventory wordt gesloten");
                if (pickUpSlot.IsSlotInUse())
                {
                    GameObject prefab = pickUpSlot.GetToolPrefab();
                    Debug.Log($"[InventoryManagerNew] Prefab bij sluiten inventory: {prefab}");
                    toolDisplayManager.ShowTool(prefab);
                }
                else
                {
                    Debug.Log("[InventoryManagerNew] Geen item in gebruik bij sluiten inventory");
                }
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
        if (toolDatabase == null)
        {
            Debug.LogError("[InventoryManagerNew] ToolDatabase is niet gezet.");
            return null;
        }
        return toolDatabase.GetPrefabForItem(itemName);
    }

    public string GetCurrentToolName()
    {
        if (pickUpSlot != null && pickUpSlot.IsSlotInUse())
        {
            return pickUpSlot.GetItemName();
        }
        return null;
    }

    public void ConsumeCurrentTool()
    {
        if (pickUpSlot != null && pickUpSlot.IsSlotInUse())
        {
            pickUpSlot.ConsumeTool();
        }
    }

}

public enum ItemType
{
    hint,
    tool
};
