using UnityEngine;

public class ShowInteractPrompt : MonoBehaviour
{
    private GameObject parent;
    private GameObject instantiatedIcon;

    public GameObject interactPromptIconPrefab;
    public Sprite icon;
    public Positioning positioning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateParentObjectAndPutCurrentObjectIn();
        addIconPrefabToParentGroup();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = Camera.main.transform.position;
        Vector3 dir = Camera.main.transform.forward;

        float maxDistance = 100f;
        float sphereRadius = 0.3f;

        if (Physics.SphereCast(origin, sphereRadius, dir, out RaycastHit hit, maxDistance))
        {
            if (hit.collider == GetComponent<Collider>() && instantiatedIcon is not null)
            {
                instantiatedIcon.SetActive(true);
            }
            else
            {
                instantiatedIcon.SetActive(false);
            }
        }
    }

    private void CreateParentObjectAndPutCurrentObjectIn()
    {
        // Create a new parent GameObject
        parent = new GameObject("Group");

        // Store the current position and rotation of the child
        Vector3 childPosition = transform.position;

        // Set the parent of the current object
        transform.SetParent(parent.transform, true);

        // Set the parent's position and rotation to the child's original
        parent.transform.position = childPosition;

        // Reset the child's local position and rotation
        transform.localPosition = Vector3.zero;
    }

    private void addIconPrefabToParentGroup()
    {
        if (interactPromptIconPrefab is not null)
        {
            Vector3 position;

            switch (positioning)
            {
                case Positioning.Up:
                    position = Vector3.up;
                    break;
                case Positioning.Front:
                    position = Vector3.back;
                    break;
                default:
                    position = Vector3.up;
                    break;
            }

            instantiatedIcon = Instantiate(interactPromptIconPrefab);

            instantiatedIcon.transform.SetParent(parent.transform, true);
            instantiatedIcon.transform.localPosition = position;
            instantiatedIcon.SetActive(false);
        }
    }
}

public enum Positioning
{
    Up,
    Front
}
