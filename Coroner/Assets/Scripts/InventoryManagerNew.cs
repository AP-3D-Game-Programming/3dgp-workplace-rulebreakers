using UnityEngine;


public class InventoryManagerNew : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public static InventoryManagerNew Instance;
    public ItemSlot[] itemSlot;
    public ScriptObjItem[] scriptObjItems;

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

    public void AddItem(string itemName, Sprite inventoryIcon, string itemDescription)
    {
        Debug.Log("ItemName = " + itemName + " - itemSprite = " + inventoryIcon);
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, inventoryIcon, itemDescription);
                return;
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

    }
}
