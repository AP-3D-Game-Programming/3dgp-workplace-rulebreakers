using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BodyExaminationManager : MonoBehaviour
{
    [Header("Settings")]
    public int totalParts = 4; // totaal aantal lichaamsdelen (kan ik later aanpassen)
    private int examinedParts = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI text;
    public Button restartButton;
    public MouseMovement mouseMovement;

    private void Start()
    {
        text.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    public void PartExamined()
    {
        examinedParts++;

        if (examinedParts >= totalParts)
        {
            if (mouseMovement != null) mouseMovement.SetLocked(false);
            text.text = "All body parts examined! Well done!";
            text.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }
}
