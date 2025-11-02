using UnityEngine;

public class ExamineBodyPart : MonoBehaviour
{
    private InventoryManagerNew inventory;

    [Tooltip("Tag van het juiste instrument (bijv. 'Pincet' of 'Scalpel')")]
    public string requiredToolTag;

    public GameObject successPrefab;
    public GameObject failPrefab;

    void Start()
    {
        inventory = InventoryManagerNew.Instance;
    }

    private void OnMouseDown()
    {
        string currentToolName = inventory.GetCurrentToolName();

        if (string.IsNullOrEmpty(currentToolName))
        {
            Debug.LogWarning("Geen tool geselecteerd!");
            return;
        }

        Debug.Log($"[ExamineBodyPart] Current tool: {currentToolName}, required: {requiredToolTag}");


        if (currentToolName == requiredToolTag)
        {
            Instantiate(successPrefab, transform.position, transform.rotation, transform);
            Debug.Log($"CORRECT! {gameObject.name} was clicked with matching tool '{currentToolName}'.");


            inventory.ConsumeCurrentTool();
        }
        else
        {
            Instantiate(failPrefab, transform.position, Quaternion.identity);
            Debug.LogError($"Wrong tool! Needed '{requiredToolTag}', but used '{currentToolName}'.");
        }
    }
}