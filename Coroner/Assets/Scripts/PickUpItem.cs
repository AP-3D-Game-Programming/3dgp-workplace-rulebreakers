using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string itemName; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(itemName);
            Debug.Log(itemName + " toegevoegd aan inventaris!");
            gameObject.SetActive(false);
        }
    }
}
