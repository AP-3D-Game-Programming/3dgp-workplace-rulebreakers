using UnityEngine;
using TMPro;

public class PickUpItem : MonoBehaviour
{
    public string itemName = "Tool";
    public TextMeshProUGUI pickupText;
    public float messageDuration = 2f;
    public AudioClip pickupSound;
    public float soundVolume = 0.8f;

    private AudioSource audioSource;

    void Start()
    {
        
        if (pickupText == null)
            pickupText = GameObject.Find("PickupMessage")?.GetComponent<TextMeshProUGUI>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayPickupSound();
            ShowPickupMessage("Je hebt een " + itemName + " gevonden!");
            ShowPickupMessage(itemName + " toegevoegd aan de inventaris!");
            InventoryManager.Instance.AddItem(itemName);
            gameObject.SetActive(false);
        }
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
