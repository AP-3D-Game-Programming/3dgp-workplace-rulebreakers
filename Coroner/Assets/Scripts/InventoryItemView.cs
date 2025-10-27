using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemView : MonoBehaviour
{
    [Header("UI References")]
    public Image icon;
    public TextMeshProUGUI label;


    public void Bind(string itemName, Sprite itemIcon = null)
    {
        if (label != null)
            label.text = itemName;

        if (icon != null)
        {
            if (itemIcon != null)
            {
                icon.sprite = itemIcon;
                icon.enabled = true;
            }
            else
            {
                icon.enabled = false;
            }
        }
    }
}