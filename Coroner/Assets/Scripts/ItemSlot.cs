using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public Sprite inventoryIcon;
    public bool isFull;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManagerNew inventoryManager;

    public Image ItemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public string itemDescription;

    public Sprite emptySprite;

    public ItemType itemType;

    [SerializeField]
    private PickUpSlot pickUpSlot;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagerNew>();
    }


    public void AddItem(string itemName, Sprite inventoryIcon, string itemDescription, ItemType itemType)
    {
        this.itemName = itemName;
        this.inventoryIcon = inventoryIcon;
        this.itemDescription = itemDescription;
        this.itemType = itemType;
        isFull = true;

        itemImage.sprite = inventoryIcon;
        Debug.Log(itemName + " toegevoegd aan slot");
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
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

    public void OnLeftClick()
    {
        if (thisItemSelected)
        {
            if (itemType == ItemType.tool && pickUpSlot != null)
            {
                pickUpSlot.PickUpTool(itemName, inventoryIcon, itemDescription);
                Debug.Log("Item opgepakt: " + itemName);
            }

            else if (itemType == ItemType.hint)
            {
                inventoryManager.UseItem(itemName);
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots();
        }
            
        
        selectedShader.SetActive(true);
        thisItemSelected = true;

        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        ItemDescriptionImage.sprite = inventoryIcon;

        if (ItemDescriptionImage.sprite == null)
        {
            ItemDescriptionImage.sprite = emptySprite;
        }
    }

    private void PickUpTool()
    {
        if(itemType == ItemType.tool)
        {
            pickUpSlot.PickUpTool(itemName, inventoryIcon, itemDescription);
        }
    }

    public void OnRightClick()
    {

    }
}
