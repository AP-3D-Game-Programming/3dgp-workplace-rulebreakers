using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5f;           // aanpasbare snelheid in Inspector
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // lees input (standaard Unity axes: Horizontal = A/D, Vertical = W/S)
        float h = Input.GetAxisRaw("Horizontal"); // -1,0,1
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, 0f, v);

        // normeren zodat diagonalen niet sneller zijn
        if (move.magnitude > 1f) move = move.normalized;

        // verplaats met physics (FixedUpdate)
        Vector3 newPos = rb.position + move * speed * Time.fixedDeltaTime;
     
        if (move.sqrMagnitude > 0.01f)
            rb.rotation = Quaternion.LookRotation(move);
        
        rb.MovePosition(newPos);

    }
}
