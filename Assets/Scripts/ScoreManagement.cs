using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.IO.Ports;

public class ScoreManagement : MonoBehaviour
{
    bool hasSwitchedScene = false;

    //------------- Overfør variabler


    public static ScoreManagement Instance;
    public static int overførKatCounter; // Variablen der overføres
    public static int overførNigerCounter; // Variablen der overføres



    

    //-------------------------

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 2)
        {
            SceneManager.LoadScene("Fishing Scene");
            hasSwitchedScene = true;

        }
        if(hasSwitchedScene){
            transform.position = new Vector3(100,100,100);
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Tjek, om det rørte objektet har tagget "Fiskekrog"
        if (other.CompareTag("NigerBait")){
            overførNigerCounter = overførNigerCounter+1;
        } else if(other.CompareTag("Cat")){
            overførKatCounter = overførKatCounter+1;
        }
    }
    
}
