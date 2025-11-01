using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSlot : MonoBehaviour
{
    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private TMP_Text slotName;

    [SerializeField]
    private ItemType itemType = new ItemType();

    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    private bool slotInUse;

    public void PickUpTool(string itemName, Sprite itemSprite, string itemDescription)
    {
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;

        this.itemName = itemName;
        this.itemDescription = itemDescription;

        slotInUse = true;
    }

}
