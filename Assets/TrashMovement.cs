using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Maksimal hastighed for bevægelsen
    public float acceleration = 10f; // Hvor hurtigt vi når maksimal hastighed
    public float deceleration = 15f; // Hvor hurtigt vi stopper, når tasten slippes

    private Rigidbody2D rb;
    private float moveInput;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D mangler på gameobjektet!");
        }
    }

    void Update()
    {
        // Hent input fra piletasterne (eller A/D-tasterne som alternativ)
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // Hvis der er input (brugeren holder en tast nede)
        if (moveInput != 0)
        {
            // Gradvis acceleration mod maksimal hastighed
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveInput * moveSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Gradvis deceleration mod 0, når der ikke er input
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
        }

        // Opdater Rigidbody2D's hastighed for at bevæge gameobjektet
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
    }
}
