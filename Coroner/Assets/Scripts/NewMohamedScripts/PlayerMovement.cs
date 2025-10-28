using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6.5f;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Dit werkt niet goed: bij omhoog/omlaag kijken gaat de speler ook omhoog/omlaag bewegen
        //Vector3 move = transform.right * x + transform.forward * z;

        // Oplossing: negeer de Y-component zodat speler alleen horizontaal beweegt
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 move = right * x + forward * z;
        controller.Move(move * speed * Time.deltaTime);

    }
}
