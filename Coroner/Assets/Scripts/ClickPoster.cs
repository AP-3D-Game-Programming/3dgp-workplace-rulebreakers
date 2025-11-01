using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class ClickPoster : MonoBehaviour
{
    public ItemType itemType;

    [SerializeField]
    private string hintName;

    [SerializeField]
    private Sprite hintIcon;

    [SerializeField]
    private TextMeshProUGUI pickupMessage;

    public float messageDuration = 2f;

    [SerializeField]
    private AudioClip pickupSound;

    public float soundVolume = 0.8f;

    private AudioSource audioSource;

    private InventoryManagerNew inventoryManager;

    [TextArea]
    [SerializeField]
    private string hintDescription;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryPopUp").GetComponent<InventoryManagerNew>();

        if (pickupMessage == null)
            pickupMessage = GameObject.Find("PickUpMessage")?.GetComponent<TextMeshProUGUI>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnMouseUp()
    {
        Debug.Log($"clicked poster with tag '{tag}'");
        InventoryManagerNew.Instance.AddItem(hintName, hintIcon, hintDescription, itemType);
        PlayPickupSound();
        ShowPickupMessage("You foud a hint about the " + hintName + "!\nHint added to your inventory!");
        Destroy(gameObject);
    }

    void PlayPickupSound()
    {
        if (pickupSound != null)
            audioSource.PlayOneShot(pickupSound, soundVolume);
    }

    void ShowPickupMessage(string message)
    {
        if (pickupMessage == null) return;

        pickupMessage.text = message;
        CancelInvoke(nameof(ClearMessage));
        Invoke(nameof(ClearMessage), messageDuration);
    }

    void ClearMessage()
    {
        pickupMessage.text = "";
    }
}
