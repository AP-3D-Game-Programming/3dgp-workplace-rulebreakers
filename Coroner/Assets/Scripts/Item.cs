using TMPro;
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManagerNew.Instance.AddItem(itemName, inventoryIcon, itemDescription);
            PlayPickupSound();
            ShowPickupMessage("You found " + itemName + "!\nItem is added to your inventory!");
            Debug.Log("Item opgepikt: " + itemName);
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryPopUp").GetComponent<InventoryManagerNew>();

        if (pickupText == null)
            pickupText = GameObject.Find("PickUpMessage")?.GetComponent<TextMeshProUGUI>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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

