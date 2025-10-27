using UnityEngine;

[CreateAssetMenu]
public class ScriptObjItem : ScriptableObject
{
    public string itemName;
    public ItemToChange itemToChange = new ItemToChange();

    public void UseItem()
    {
        if (itemToChange == ItemToChange.bodypart)
        {
            
        }
        if (itemToChange == ItemToChange.otherItem)
        {

        }
    }

    public enum ItemToChange
    {
        none,
        bodypart,
        otherItem
    };
}
