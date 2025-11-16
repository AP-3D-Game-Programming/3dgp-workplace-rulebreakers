using UnityEngine;
using TMPro;
using System.Text;
using System;
using Coroner;
public class ObjectivesManager : MonoBehaviour
{
    public TMP_Text objectivesText;
    public GameObject objectivesPanel;
    public Objective[] objectives;
    public void SetObjectivesVisible(bool visible)
    {
        if (objectivesPanel != null)
            objectivesPanel.SetActive(visible);
        if (objectivesText != null)
            objectivesText.gameObject.SetActive(visible);
    }

    void Start()
    {
        objectives = new Objective[]
        {
            new Objective("Collect the tools", false, new Objective[]
            {
                new Objective("Verzamel het scalpel"),
                new Objective("Verzamel de scissors"),
                new Objective("Verzamel de tweezers"),
                new Objective("Verzamel de forceps")
            }),
            new Objective("Inspect the body", false, new Objective[]
            {
                new Objective("Inspecteer het hoofd"),
                new Objective("Inspecteer het hand"),
                new Objective("Inspecteer de borst"),
                new Objective("Inspecteer de benen")
            })
        };
        UpdateObjectivesUI();
    }

    public void CompleteObjective(string description)
    {
        string descLower = description.ToLower();
        foreach (var obj in objectives)
        {
            if (obj.description.ToLower() == descLower)
            {
                obj.completed = true;
                UpdateObjectivesUI();
                return;
            }
            if (obj.subObjectives != null)
            {
                foreach (var sub in obj.subObjectives)
                {
                    if (sub.description.ToLower() == descLower)
                    {
                        sub.completed = true;
                        bool allDone = true;
                        foreach (var s in obj.subObjectives)
                        {
                            if (!s.completed)
                            {
                                allDone = false;
                                break;
                            }
                        }
                        if (allDone)
                        {
                            obj.completed = true;
                        }
                        UpdateObjectivesUI();
                        return;
                    }
                }
            }
        }
    }

    private void UpdateObjectivesUI()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<b>Objectives</b>\n");
        foreach (var obj in objectives)
        {
            sb.AppendLine(FormatObjective(obj));
            if (obj.subObjectives != null)
            {
                foreach (var sub in obj.subObjectives)
                {
                    sb.AppendLine("    " + FormatObjective(sub));
                }
            }
        }
        if (objectivesText != null)
            objectivesText.text = sb.ToString();
    }

    private string FormatObjective(Objective obj)
    {
        if (obj.completed)
            return $"<s>{obj.description}</s>";
        else
            return obj.description;
    }
}
