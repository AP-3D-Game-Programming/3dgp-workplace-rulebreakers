using UnityEngine;

public class InteractPromptManager : MonoBehaviour
{
    public Sprite icon;
    private Transform cam;

    void Start()
    {
        icon = transform.parent.GetComponentInChildren<ShowInteractPrompt>().icon;
        gameObject.GetComponent<SpriteRenderer>().sprite = icon;
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (!transform.parent.GetChild(0).gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
