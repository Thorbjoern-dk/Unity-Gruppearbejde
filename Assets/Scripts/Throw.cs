using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour
{
    public GameObject objectToSpawn; // Det gameobjekt, der skal instantiateres
    public GameObject objectToSpawn2; // Det gameobjekt, der skal instantiateres
    public float throwForce = 10f;   // Kraften, som objektet kastes med
    public Vector2 spawnPosition1; // Position, hvor objektet spawnes
    public Vector2 spawnPosition2; // Position, hvor objektet spawnes
    public float throwAngleMin;
    public float throwAngleMax;

    public float TimeBetweenThrow;

    void Start()
    {
        // Start coroutine, som kører kontinuerligt
        StartCoroutine(SpawnObjectEveryFiveSeconds());
    }

    void FixedUpdate()
    {
        // Tjek om brugeren trykker på musens venstre knap
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    void SpawnAndThrowObjectFromRight()
    {
        // Instantiate gameobjektet på den angivne position
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition1, Quaternion.identity);

        // Tjek om objektet har en Rigidbody2D-komponent
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Tilføj en Rigidbody2D-komponent, hvis den mangler
            rb = spawnedObject.AddComponent<Rigidbody2D>();
        }

        // Kast objektet ind på skærmen med en kraft
        Vector2 throwDirection = new Vector2(Random.Range(throwAngleMin, throwAngleMax), 1f).normalized; // Tilfældig retning indad opad
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
    }



        void SpawnAndThrowObjectFromLeft()
    {
        // Instantiate gameobjektet på den angivne position
        GameObject spawnedObject = Instantiate(objectToSpawn2, spawnPosition2, Quaternion.identity);

        // Tjek om objektet har en Rigidbody2D-komponent
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Tilføj en Rigidbody2D-komponent, hvis den mangler
            rb = spawnedObject.AddComponent<Rigidbody2D>();
        }

        // Kast objektet ind på skærmen med en kraft
        Vector2 throwDirection = new Vector2(Random.Range(-throwAngleMin, -throwAngleMax), 1f).normalized; // Tilfældig retning indad opad
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
    }

    IEnumerator SpawnObjectEveryFiveSeconds()
    {
        while (true) // Loop for at køre kontinuerligt
        {
            if(Mathf.Round(Random.Range(1,3)) == 2){
                SpawnAndThrowObjectFromRight();
            } else{
                SpawnAndThrowObjectFromLeft();
            }
            yield return new WaitForSeconds(TimeBetweenThrow); 
        }
    }
}