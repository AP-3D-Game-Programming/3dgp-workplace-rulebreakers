using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class PlayerMovement : MonoBehaviour
{
    public AudioSource footstepAudio;
    public CharacterController controller;
    public float speed = 6.5f;
    public float gravity = -9.81f * 2;

    Vector3 velocity;
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

        // Speel geluid af als de speler beweegt en op de grond staat
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            footstepAudio.enabled = true;
        }
        else
        {
            footstepAudio.enabled = false;
        }

        // zwaartkracht instellen
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
