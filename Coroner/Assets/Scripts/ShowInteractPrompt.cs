using UnityEngine;

public class ShowInteractPrompt : MonoBehaviour
{
    private GameObject parent;
    public GameObject interactPromptIconPrefab;
    public Sprite icon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateParentObjectAndPutCurrentObjectIn();
        addIconPrefabToParentGroup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateParentObjectAndPutCurrentObjectIn()
    {
        // Create a new parent GameObject
        parent = new GameObject("Group");

        // Store the current position and rotation of the child
        Vector3 childPosition = transform.position;
        //Quaternion childRotation = transform.rotation;

        // Set the parent of the current object
        transform.SetParent(parent.transform, true);

        // Set the parent's position and rotation to the child's original
        parent.transform.position = childPosition;
        //parent.transform.rotation = childRotation;

        // Reset the child's local position and rotation
        transform.localPosition = Vector3.zero;
        //transform.localRotation = Quaternion.identity;
    }

    private void addIconPrefabToParentGroup()
    {
        if (interactPromptIconPrefab is not null)
        {
            GameObject instantiatedIcon = Instantiate(interactPromptIconPrefab);

            instantiatedIcon.transform.SetParent(parent.transform, true);
            instantiatedIcon.transform.localPosition = Vector3.up;
            //instantiatedIcon.transform.localRotation = Quaternion.identity;
        }
    }
}
