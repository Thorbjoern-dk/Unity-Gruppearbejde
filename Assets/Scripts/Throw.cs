using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour
{
    public GameObject objectToSpawn; // Det gameobjekt, der skal instantiateres
    public float throwForce = 10f;   // Kraften, som objektet kastes med
    public Vector2 spawnPosition = new Vector2(0, 0); // Position, hvor objektet spawnes
    public float throwAngle;

    void Start()
    {
        // Start coroutine, som kører kontinuerligt
        StartCoroutine(SpawnObjectEveryFiveSeconds());
    }

    void Update()
    {
        // Tjek om brugeren trykker på musens venstre knap
        if (Input.GetMouseButtonDown(0))
        {
            SpawnAndThrowObject();
        }
    }

    void SpawnAndThrowObject()
    {
        // Instantiate gameobjektet på den angivne position
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Tjek om objektet har en Rigidbody2D-komponent
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Tilføj en Rigidbody2D-komponent, hvis den mangler
            rb = spawnedObject.AddComponent<Rigidbody2D>();
        }

        // Kast objektet ind på skærmen med en kraft
        Vector2 throwDirection = new Vector2(Random.Range(-throwAngle, throwAngle), 1f).normalized; // Tilfældig retning indad opad
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
    }

    IEnumerator SpawnObjectEveryFiveSeconds()
    {
        while (true) // Loop for at køre kontinuerligt
        {
            SpawnAndThrowObject(); // Spawn objektet
            yield return new WaitForSeconds(5); // Vent i 5 sekunder
        }
    }
}