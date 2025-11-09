using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamineBodyPart : MonoBehaviour
{
    public TextMeshProUGUI uiLogText;

    private InventoryManagerNew inventory;

    [Tooltip("Tag van het juiste instrument (bijv. 'Pincet' of 'Scalpel')")]
    public string requiredToolTag;

    public GameObject successPrefab;
    public GameObject failPrefab;

   // M => toegevoegd door Mohamed(doe niet weg!)
   public BodyExaminationManager manager;


    void Start()
    {
        inventory = InventoryManagerNew.Instance;
    }

    private void OnMouseDown()
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

            if (manager != null)
                manager.PartExamined();
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
