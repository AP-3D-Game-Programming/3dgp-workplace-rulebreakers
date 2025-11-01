using UnityEngine;

[CreateAssetMenu]
public class ScriptObjItem : ScriptableObject
{
    public string itemName;
    public ItemToChange itemToChange = new ItemToChange();

    public void UseItem()
    {
        if (itemToChange == ItemToChange.face)
        {
            
        }
        if (itemToChange == ItemToChange.brain)
        {

        }
    }

    public enum ItemToChange
    {
        face,
        brain,
        limb,
        torso
    };
}
