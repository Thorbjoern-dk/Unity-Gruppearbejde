using UnityEngine;

public class MoveFiskekrog : MonoBehaviour
{
    public float moveSpeed = 5f; // Maksimal hastighed for bevægelsen
    public float acceleration = 10f; // Hvor hurtigt vi når maksimal hastighed
    public float deceleration = 15f; // Hvor hurtigt vi stopper, når tasten slippes

    public float fallSpeed;

    private Rigidbody2D rb;
    private float moveInput;
    private float currentSpeed;

    private bool HasCaughtBait = false;

    void Start()
    {
        HasCaughtBait = false;
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
        if(HasCaughtBait==false && Input.GetKey("space")){
            HasCaughtBait = true;
            fallSpeed = fallSpeed*-1;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fallSpeed);
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
