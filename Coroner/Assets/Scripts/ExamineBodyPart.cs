using UnityEngine;

public class ExamineBodyPart : MonoBehaviour
{
    private InventoryManager inventory;
    public GameObject successPrefab;
    public GameObject failPrefab;

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
            Instantiate(successPrefab, transform.position, transform.rotation, transform);
            Debug.Log($"CORRECT! {gameObject.name} was clicked with matching tag '{tag}'.");
        }
        else
        {
            Instantiate(failPrefab, transform.position, Quaternion.identity);
            Debug.LogError("Wrong tag, try another one!");
        }
    }
}
