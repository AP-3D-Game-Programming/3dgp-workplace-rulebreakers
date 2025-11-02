using UnityEngine;

public class ToolDisplayManager : MonoBehaviour
{
    public Transform toolAnchor;
    private GameObject currentTool;

    public void ShowTool(GameObject toolPrefab)
    {
        if (currentTool != null)
        {
            Destroy(currentTool);
        }

        currentTool = Instantiate(toolPrefab, toolAnchor.position, toolAnchor.rotation, toolAnchor);
    }

    public void HideTool()
    {
        if (currentTool != null)
        {
            Destroy(currentTool);
            currentTool = null;
        }
    }
}
