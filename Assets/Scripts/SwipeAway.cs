using UnityEngine;

public class SwipeAway : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startMousePos;
    private Vector2 endMousePos;
    private bool isSwiping = false;

    // Maximal kraft for meget lange swipes
    [SerializeField] private float maxForce = 40f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on the object.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverObject())
            {
                isSwiping = true;
                startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("Swipe Start: " + startMousePos);
            }
        }

        if (isSwiping && Input.GetMouseButton(0))
        {
            endMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Swipe Progress: " + endMousePos);
        }

        if (isSwiping && Input.GetMouseButtonUp(0))
        {
            Vector2 swipeDirection = endMousePos - startMousePos;
            Debug.Log("Swipe End: " + swipeDirection);

            if (swipeDirection.magnitude > 0.1f)
            {
                // Kraften skaleres med swipe-afstanden
                float forceMagnitude = Mathf.Clamp(swipeDirection.magnitude * 3.5f, 0, maxForce);
                Vector2 appliedForce = swipeDirection.normalized * forceMagnitude;

                // Anvend den beregnede kraft
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(appliedForce, ForceMode2D.Impulse);

                Debug.Log($"Applying Force: {appliedForce} (Magnitude: {forceMagnitude})");
            }
            else
            {
                Debug.Log("Swipe direction too small to apply force.");
            }

            isSwiping = false;
        }
    }

    bool IsMouseOverObject()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = GetComponent<Collider2D>();

        return collider.OverlapPoint(mousePos);
    }
}