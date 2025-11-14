using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private Sprite inventoryIcon;

    [SerializeField]
    private TextMeshProUGUI pickupText;

    public float messageDuration = 2f;

    [SerializeField]
    private AudioClip pickupSound;

    public float soundVolume = 0.8f;

    private AudioSource audioSource;

    private InventoryManagerNew inventoryManager;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    [SerializeField]
    private bool isClickable = false;

    public ItemType itemType;


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!isClickable && other.CompareTag("Player"))
    //    {
    //        InventoryManagerNew.Instance.AddItem(itemName, inventoryIcon, itemDescription, itemType);
    //        PlayPickupSound();
    //        ShowPickupMessage("You found " + itemName + "!\nItem is added to your inventory!");
    //        Debug.Log("Item opgepikt: " + itemName);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnMouseUp()
    {
        if (isClickable)
        {
            Debug.Log($"clicked poster with tag '{tag}'");
            InventoryManagerNew.Instance.AddItem(itemName, inventoryIcon, itemDescription, itemType);
            PlayPickupSound();
            ShowPickupMessage("You foud a hint about the " + itemName + "!\nHint added to your inventory!");
            Destroy(gameObject);
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagerNew>();

        if (pickupText == null)
            pickupText = GameObject.Find("PickUpMessage")?.GetComponent<TextMeshProUGUI>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    // ----- door Mohamed ----- //
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // linkermuisklik
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    PickiUpItem();
                }
            }
        }
    }
    void PickiUpItem()
    {
        ShowPickupMessage("Je hebt een " + itemName + " gevonden!");
        ShowPickupMessage(itemName + " toegevoegd aan de inventaris!");
        PlayPickupSound();
        InventoryManagerNew.Instance.AddItem(itemName, inventoryIcon, itemDescription, itemType);

        var objectivesManager = FindFirstObjectByType<ObjectivesManager>();
        if (objectivesManager != null && itemType == ItemType.tool)
        {
            objectivesManager.CompleteObjective("Collect the " + itemName);
        }

        gameObject.SetActive(false);
    }

    void PlayPickupSound()
    {
        if (pickupSound != null)
            audioSource.PlayOneShot(pickupSound, soundVolume);
    }

    void ShowPickupMessage(string message)
    {
        if (pickupText == null) return;

        pickupText.text = message;
        CancelInvoke(nameof(ClearMessage));
        Invoke(nameof(ClearMessage), messageDuration);
    }

    void ClearMessage()
    {
        pickupText.text = "";
    }
}

