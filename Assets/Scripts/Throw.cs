using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System.Collections;

public class Throw : MonoBehaviour
{
    public static float OverførtKatte;
    public static float OverførtPrince;

    public float showKat;

    

    public GameObject objectToSpawn; // Det gameobjekt, der skal instantiateres
    public GameObject objectToSpawn2; // Det gameobjekt, der skal instantiateres
    public GameObject Kitty; // Det gameobjekt, der skal instantiateres

    
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
        showKat = OverførtKatte;

    }

        void SpawnAndThrowObject()
    {
        

        if(Random.value > 0.5f && OverførtPrince>=0){//spawn prince
            GameObject spawnedObject = null;
            if(Random.Range(0,2)==1){
                spawnedObject = Instantiate(objectToSpawn, spawnPosition1, Quaternion.identity);
            } else{
                spawnedObject = Instantiate(objectToSpawn2, spawnPosition1, Quaternion.identity);
            }
            
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if(Random.value > 0.5f){ //spawn venstre
                spawnedObject.transform.position = spawnPosition2;
                Vector2 throwDirection = new Vector2(Random.Range(-throwAngleMin, -throwAngleMax), 1f).normalized; 
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }else{//spawn til højre
                spawnedObject.transform.position = spawnPosition1;
                Vector2 throwDirection = new Vector2(Random.Range(throwAngleMin, throwAngleMax), 1f).normalized; 
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }
            OverførtPrince = OverførtPrince -1;
        } else if(OverførtKatte>=0){//spawn Cat
            GameObject spawnedObject = null;    
            spawnedObject = Instantiate(Kitty, spawnPosition2, Quaternion.identity);
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if(Random.value > 0.5f){ //spawn venstre
                spawnedObject.transform.position = spawnPosition2;
                Vector2 throwDirection = new Vector2(Random.Range(-throwAngleMin, -throwAngleMax), 1f).normalized;
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }else{//spawn til højre
                spawnedObject.transform.position = spawnPosition1;
                Vector2 throwDirection = new Vector2(Random.Range(throwAngleMin, throwAngleMax), 1f).normalized;
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }
            OverførtKatte = OverførtKatte -1;
        }


    }

    IEnumerator SpawnObjectEveryFiveSeconds()
    {
        while (OverførtKatte + OverførtPrince > 0) // Loop for at køre kontinuerligt
        {
            SpawnAndThrowObject();
            yield return new WaitForSeconds(TimeBetweenThrow); 
        }
    }
}