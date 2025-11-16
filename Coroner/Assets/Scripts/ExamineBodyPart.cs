using System.Collections;
using TMPro;
using UnityEngine;

public class ExamineBodyPart : MonoBehaviour
{
    public TextMeshProUGUI uiLogText;

    private InventoryManagerNew inventory;
    private BoxCollider collider;

    [Tooltip("Tag van het juiste instrument (bijv. 'Pincet' of 'Scalpel')")]
    public string requiredToolTag;

    public GameObject successPrefab;
    public GameObject failPrefab;

   // M => toegevoegd door Mohamed(doe niet weg!)
   public BodyExaminationManager manager;


    void Start()
    {
        inventory = InventoryManagerNew.Instance;
        collider = gameObject.GetComponent<BoxCollider>();

        // Synchronize boundaries gameObject with its BoxCollider
        transform.SetPositionAndRotation(collider.transform.position, collider.transform.rotation);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera cam = Camera.main;
            if (cam == null) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (collider.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider != null && hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
                    HandleClick();
            }
        }
    }

    private void HandleClick()
    {
        if (inventory == null)
        {
            Debug.LogWarning("[ExamineBodyPart] Inventory is null!");
            return;
        }

        string currentToolName = inventory.GetCurrentToolName();
        string message = "";

        // Eerste check: geen tool geselecteerd -> toon message en stop
        if (string.IsNullOrEmpty(currentToolName))
        {
            message = "Geen tool geselecteerd! Lichaamsdeel: " + gameObject.tag;
            Debug.Log(message);

            if (uiLogText != null)
            {
                uiLogText.gameObject.SetActive(true);
                uiLogText.text = message;
                StartCoroutine(WaitAndSetTextInActive());
            }

            return; // <- heel belangrijk: voorkom overschrijven van message
        }

        // Vanaf hier weet je dat er wél een tool geselecteerd is
        if (currentToolName == requiredToolTag)
        {
            message = "CORRECT! " + gameObject.name + " onderzocht met " + currentToolName;
            Debug.Log(message);

            if (successPrefab != null)
                Instantiate(successPrefab, transform.position, Quaternion.identity);

            inventory.ConsumeCurrentTool();

                var toolDisplayManager = FindFirstObjectByType<ToolDisplayManager>();
                if (toolDisplayManager != null)
                    toolDisplayManager.HideTool();

            if (manager != null)
                manager.PartExamined();

            // Trigger body inspection objective completion
            var objectivesManager = FindFirstObjectByType<ObjectivesManager>();
            if (objectivesManager != null)
                objectivesManager.CompleteObjective("Inspecteer de " + gameObject.name.ToLower());
                objectivesManager.CompleteObjective("Inspecteer het " + gameObject.name.ToLower());
        }
        else
        {
            message = "WRONG TOOL! Vereist: " + requiredToolTag + " Gebruikt: " + currentToolName;
            Debug.Log(message);

            if (failPrefab != null)
                Instantiate(failPrefab, transform.position, Quaternion.identity);
        }

        // Toon message in UI
        if (uiLogText != null)
        {
            uiLogText.gameObject.SetActive(true);
            uiLogText.text = message;
            StartCoroutine(WaitAndSetTextInActive());
        }
    }

    private IEnumerator WaitAndSetTextInActive()
    {
        yield return new WaitForSeconds(3f);
        if (uiLogText != null)
            uiLogText.gameObject.SetActive(false);
    }
}
