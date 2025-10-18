using UnityEngine;

public class ExamineBodyPart : MonoBehaviour
{
    InventoryManager inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseUp()
    {
        GameObject currentInstrument = GameObject.Find(inventory.currentItem);

        if (CompareTag(currentInstrument.tag))
        {
            Debug.Log($"CORRECT! {gameObject.name} was clicked with matching tag '{tag}'.");
        }
        else
        {
            Debug.LogError("Wrong tag, try another one!");
        }
    }
}
