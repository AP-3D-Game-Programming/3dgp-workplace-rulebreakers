using UnityEngine;

public class ExamineBodyPart : MonoBehaviour
{
    private InventoryManagerNew inventory;

    [Tooltip("Tag van het juiste instrument (bijv. 'Pincet' of 'Scalpel')")]
    public string requiredToolTag;

    public GameObject successPrefab;
    public GameObject failPrefab;

   // M => toegevoegd door Mohamed(doe niet weg!)
   //  public BodyExaminationManager manager;


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

            // M
            //if (manager != null)
            //    manager.PartExamined();
            return;

        }

        Debug.Log($"[ExamineBodyPart] Current tool: {currentToolName}, required: {requiredToolTag}");


        if (currentToolName == requiredToolTag)
        {
            Debug.Log($"CORRECT! {gameObject.name} was clicked with matching tool '{currentToolName}'.");

            if (successPrefab != null)
                Instantiate(successPrefab, transform.position, Quaternion.identity);
                Debug.Log("Sparkle prefab instantiated!");

            inventory.ConsumeCurrentTool();


        }
        else
        {
            Debug.LogError($"Wrong tool! Needed '{requiredToolTag}', but used '{currentToolName}'.");

            if (failPrefab != null)
                Instantiate(failPrefab, transform.position, Quaternion.identity);
        }

    }
}