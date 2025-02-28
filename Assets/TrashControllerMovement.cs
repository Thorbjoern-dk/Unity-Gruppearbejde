using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.IO.Ports;

public class TrashControllerMovement : MonoBehaviour
{

    SerialPort serialPort; 
    private Rigidbody2D rb;

    public float moveInput;

    public float acceleration = 10f; // Hvor hurtigt vi når maksimal hastighed

    private float currentSpeed;
    public float turnSpeed;

    public bool startCheck;

    public GameObject SwapTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();


        serialPort = new("COM4", 115200){
        WriteTimeout = 50,
        ReadTimeout = 2,
        DtrEnable = true,
        RtsEnable = true
        };
        
        serialPort.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine().Trim();
                Debug.Log("Received: " + data); // Debug for at se input
                if (data == "LEFT") {
                    moveInput = Mathf.Lerp(moveInput, -10f, Time.deltaTime * turnSpeed);
                    
                } 
                else if (data == "RIGHT") {
                    moveInput = Mathf.Lerp(moveInput, 10f, Time.deltaTime * turnSpeed);
                } else{
                    Debug.Log("null");
                }

                
                if (data == "BUTTON_PRESS" || Input.GetKeyDown(KeyCode.Space)){
                    if(transform.position.y==-3){
                        transform.position = new Vector3(transform.position.x,20,transform.position.z);
                    } else{
                        transform.position = new Vector3(transform.position.x,-3,transform.position.z);
                }
                    
                }
            }

            catch (TimeoutException) {              
            }
        }


    }
    void FixedUpdate(){
        if (moveInput != 0)
        {
            // Gradvis acceleration mod maksimal hastighed
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveInput, acceleration * Time.fixedDeltaTime);
        }
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
        moveInput = moveInput * Time.deltaTime * 40;
    }
}
