using UnityEngine;

public class Desacelerate : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Drag;
    private Transform targetTransform; // Referencen til det objekt, der skal følges
    private bool isFollowing = false; // Om objektet skal følge mål-objektet

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearDamping = Drag; // Indstil luftmodstanden
        }
        else
        {
            Debug.LogError("Rigidbody2D mangler på gameobjektet!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Kollideret");
        // Tjek, om det rørte objektet har tagget "Fiskekrog"
        if (other.CompareTag("Fiskekrog"))
        {
            Debug.Log("Kollideret med krog");
            targetTransform = other.transform; // Gem referencen til objektets transform
            isFollowing = true; // Aktivér følgetilstand
        }
    }

    void Update()
    {
        // Hvis vi følger et mål, opdater positionen til mål-objektets position
        if (isFollowing && targetTransform != null)
        {
            transform.position = targetTransform.position;
            Debug.Log("Følger objektet: " + targetTransform.name);
        }
    }
}