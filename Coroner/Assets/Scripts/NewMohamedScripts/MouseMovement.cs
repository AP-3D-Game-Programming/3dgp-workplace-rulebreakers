using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //control rotation around x acis (look up and down)
        xRotation -= mouseY;

        //we clamp the rotation so we cant over-rotate (like in real life)
        xRotation = Mathf.Clamp(xRotation, -90, 90f); //min-max waarden

        //control rotation around y axis (look up and down)
        yRotation += mouseX; //when moving the mouse left and right we need to rotate around the y Axis not x Axis! (the same logich with the xRotation)

        //applying both rotations
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
