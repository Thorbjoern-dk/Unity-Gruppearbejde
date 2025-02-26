using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.IO.Ports;

public class MoveFiskekrog : MonoBehaviour
{
    
    public float moveSpeed = 5f; // Maksimal hastighed for bevægelsen
    public float acceleration = 10f; // Hvor hurtigt vi når maksimal hastighed
    

    public float fallSpeed;

    private Rigidbody2D rb;
    public float moveInput;
    private float currentSpeed;

    private bool HasCaughtBait = false;

    public float KatCounter;
    public float NigerPrinceCounter;
    //------------- Overfør variabler


    public static MoveFiskekrog Instance;
    public int overførKatCounter; // Variablen der overføres
    public int overførNigerCounter; // Variablen der overføres


    //-------------------------
    public TextMeshProUGUI scoreText;


    //controller
    SerialPort serialPort; 

    public float turnSpeed;
    public float deceleration = 1f;


    

    void Start()
    {
        HasCaughtBait = false;
        rb = GetComponent<Rigidbody2D>();


        serialPort = new("COM4", 115200){
        WriteTimeout = 100,
        ReadTimeout = 1,
        DtrEnable = true,
        RtsEnable = true
        };
        
        serialPort.Open();

    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine().Trim();
                Debug.Log("Received: " + data); // Debug for at se input
                if (data == "LEFT") {
                    moveInput = Mathf.Lerp(moveInput, -5f, Time.deltaTime * turnSpeed);
                    
                } 
                else if (data == "RIGHT") {
                    moveInput = Mathf.Lerp(moveInput, 5f, Time.deltaTime * turnSpeed);
                } else{
                    Debug.Log("null");
                }

                
                if (data == "BUTTON_PRESS" && !HasCaughtBait){
                    HasCaughtBait = true;
                    fallSpeed *= -1;
                    
                }
            }

            catch (TimeoutException) {              
            }
        }

    scoreText.text = "Score: " + (KatCounter - NigerPrinceCounter);

}

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fallSpeed);
        // Hvis der er input (brugeren holder en tast nede)
        if (moveInput != 0)
        {
            // Gradvis acceleration mod maksimal hastighed
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveInput, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Gradvis deceleration mod 0, når der ikke er input
            //currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
        }

        // Opdater Rigidbody2D's hastighed for at bevæge gameobjektet
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);

        


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Tjek, om det rørte objektet har tagget "Fiskekrog"
        if (other.CompareTag("NigerBait")){
            NigerPrinceCounter = NigerPrinceCounter+1;
        } else if(other.CompareTag("Cat")){
            KatCounter = KatCounter+1;
        }
    }
}
