using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    //float yRotation = 0f;
    public bool locked;

    public Transform playerBody; // moet naar de speler verwijzen
    // public static MouseMovement Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
        locked = true;
    }

    // Update is called once per frame
    void Update()
    {
        // dit stuk niet meer nodig
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    locked = !locked;
        //    Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;

        //}

        if (!locked) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //control rotation around x acis (look up and down)
        xRotation -= mouseY;

        //we clamp the rotation so we cant over-rotate (like in real life)
        xRotation = Mathf.Clamp(xRotation, -90, 90f); //min-max waarden

        //control rotation around y axis (look up and down)
        //yRotation += mouseX; //when moving the mouse left and right we need to rotate around the y Axis not x Axis! (the same logich with the xRotation)

        //applying both rotations
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // speler rotatie (links/rechts)
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void SetLocked(bool shouldLock)
    {
        locked = shouldLock;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}
