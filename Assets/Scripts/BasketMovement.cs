using UnityEngine;

public class BasketMovement : MonoBehaviour
{

    public bool startCheck;

    public Transform SwapTransform;
    public float moveSpeed = 5f; // Maksimal hastighed for bevægelsen
    public float acceleration = 10f; // Hvor hurtigt vi når maksimal hastighed
    public float deceleration = 15f; // Hvor hurtigt vi stopper, når tasten slippes

    private Rigidbody2D rb;
    private float moveInput;
    private float currentSpeed;

    Vector3 startpos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D mangler på gameobjektet!");
        }
        startpos = transform.position;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(startCheck){
                startCheck=false;
            } else{
                startCheck=true;
            }
        }
        if(startCheck){
            transform.position = new Vector3(SwapTransform.transform.position.x,-3, transform.position.z);
        } else{
            transform.position = new Vector3(SwapTransform.transform.position.x,20, transform.position.z);
        }
        
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
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
