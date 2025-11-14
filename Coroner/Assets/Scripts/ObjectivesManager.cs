using UnityEngine;
using TMPro;
using System.Text;
using System;
using Coroner;
public class ObjectivesManager : MonoBehaviour
{
    public TMP_Text objectivesText;
    public Objective[] objectives;

    void Start()
    {
        objectives = new Objective[]
        {
            new Objective("Collect the tools", false, new Objective[]
            {
                new Objective("Collect the scalpel"),
                new Objective("Collect the scissors"),
                new Objective("Collect the tweezers"),
                new Objective("Collect the forceps")
            }),
            new Objective("Inspect the body", false, new Objective[]
            {
                new Objective("Inspect the brain"),
                new Objective("Inspect the face"),
                new Objective("Inspect the limbs"),
                new Objective("Inspect the torso")
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
