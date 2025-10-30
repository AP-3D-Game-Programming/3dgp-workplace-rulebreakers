using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator animator;
    private bool isOpen = false;
    private bool playerNear = false;

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            animator.Play(isOpen ? "DoorOpen" : "DoorClose");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
