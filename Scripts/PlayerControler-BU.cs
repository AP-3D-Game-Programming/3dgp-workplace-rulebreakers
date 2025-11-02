using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed = 5f;                 // aanpasbare snelheid in Inspector
    public string freeLookName = "FreeLook Camera"; // fallback naam (niet verplicht)
    public Camera cam;                       // als je Main Camera wilt gebruiken, laat leeg in Inspector

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // probeer Camera.main, anders zoek op naam
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam == null)
        {
            GameObject go = GameObject.Find(freeLookName);
            if (go != null)
            {
                cam = go.GetComponentInChildren<Camera>() ?? go.GetComponent<Camera>();
                // als er geen Camera component is, gebruik de FreeLook-gameobject transform als fallback
                if (cam == null)
                    Debug.LogWarning("Geen Camera gevonden op MainCamera of FreeLook Camera; movement wordt world-georiënteerd.");
            }
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D
        float v = Input.GetAxisRaw("Vertical");   // W/S

        // basis movement (relatief aan camera yaw)
        Vector3 camForward;
        Vector3 camRight;

        if (cam != null)
        {
            camForward = cam.transform.forward;
            camForward.y = 0f;
            if (camForward.sqrMagnitude < 0.0001f) camForward = Vector3.forward;
            camForward.Normalize();

            camRight = Vector3.Cross(Vector3.up, camForward).normalized;
        }
        else
        {
            // fallback: world-oriëntatie
            camForward = Vector3.forward;
            camRight = Vector3.right;
        }

        Vector3 move = camForward * v + camRight * h;

        // normeren zodat diagonalen niet sneller zijn
        if (move.magnitude > 1f) move = move.normalized;

        Vector3 newPos = rb.position + move * speed * Time.fixedDeltaTime;

        // directe rotatie naar bewegingrichting (als er beweging is)
        if (move.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(move);
            rb.MoveRotation(targetRot); // physics-friendly directe rotatie
        }

        rb.MovePosition(newPos);
    }
}
