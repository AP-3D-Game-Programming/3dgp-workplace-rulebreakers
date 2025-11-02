using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUpSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private ItemType itemType = new ItemType();

    private Sprite itemSprite;
    
    private bool slotInUse;

    [SerializeField]
    private GameObject selectedShader;

    [SerializeField]
    private bool thisItemSelected;

    [SerializeField]
    private Sprite emptySprite;

    private ItemSlot originalSlot;

    private string itemName;
    private string itemDescription;

    [SerializeField]
    private ToolDisplayManager toolDisplayManager;

    [SerializeField]
    private InventoryManagerNew inventoryManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    void OnLeftClick()
    {
        if (thisItemSelected && slotInUse)
        {
            LayDownTool();
        }
        else
        {
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }

    void OnRightClick()
    {

    }

    public void LayDownTool()
    {
        ItemSlot targetSlot = null;

        if (originalSlot != null && !originalSlot.isFull)
        {
            targetSlot = originalSlot;
        }
        else
        {
            targetSlot = inventoryManager.FindEmptySlot();
        }

        if (targetSlot != null)
        {
            targetSlot.AddItem(itemName, itemSprite, itemDescription, itemType);
        }
        else
        {
            Debug.LogWarning("Geen lege slots beschikbaar - item blijft in de hand");
            return;
        }

        itemName = "";
        itemDescription = "";
        itemSprite = emptySprite;
        slotImage.sprite = emptySprite;
        slotInUse = false;
        thisItemSelected = false;
        selectedShader.SetActive(false);
    }

    public void PickUpTool(string itemName, Sprite itemSprite, string itemDescription, ItemSlot sourceSlot)
    {
        if (slotInUse && originalSlot != null)
        {
            originalSlot.AddItem(this.itemName, this.itemSprite, this.itemDescription, itemType);
        }

        this.itemSprite = itemSprite;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.originalSlot = sourceSlot;
        
        slotImage.sprite = this.itemSprite;
        slotInUse = true;
        selectedShader.SetActive(true);
        thisItemSelected = true;

        Debug.Log($"[PickUpSlot] Picked up tool: {itemName}");
    }

    public bool IsSlotInUse()
    {
        return slotInUse;
    }

    public GameObject GetToolPrefab()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("[PickUpSlot] InventoryManager is niet ingesteld!");
            return null;
        }

        Debug.Log($"[PickUpSlot] Opvragen prefab voor item: {itemName}");
        return inventoryManager.GetPrefabForItem(itemName);
    }

    public void ConsumeTool()
    {
        itemName = "";
        itemDescription = "";
        itemSprite = emptySprite;
        slotImage.sprite = emptySprite;
        slotInUse = false;
        thisItemSelected = false;
        selectedShader.SetActive(false);
        originalSlot = null;

        toolDisplayManager.HideTool();
    }

}
