using System;
using System.IO.Ports;
using UnityEngine;
public class JoystickMovement : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM4", 115200); //  Udskift "COM3" med din ESP32’s port!
    
    void Start()
    {
        serialPort.Open(); // Åbn seriel forbindelse
        serialPort.ReadTimeout = 100; // Undgå at fryse ved timeout
    }

    void Update()
    {
        
        if (serialPort.IsOpen)
        {
            Debug.Log("Serial");
            try
            {
                string data = serialPort.ReadLine(); // Læs data fra ESP32
                string[] values = data.Split(',');

                
                Debug.Log($"Received Data: {data}"); // Print den modtagne data

                if (values.Length == 3)
                {
                    Debug.Log("Value");
                    int x = int.Parse(values[0]);
                    int y = int.Parse(values[1]);
                    int button = int.Parse(values[2]);

                    // Simulér piletaster baseret på joystickets position
                    if (x < 1500) { SendKeyPress(KeyCode.LeftArrow); }
                    else if (x > 2500) { SendKeyPress(KeyCode.RightArrow); }
                    else { ReleaseKey(KeyCode.LeftArrow); ReleaseKey(KeyCode.RightArrow); }

                    if (y < 1500) { SendKeyPress(KeyCode.DownArrow); }
                    else if (y > 2500) { SendKeyPress(KeyCode.UpArrow); }
                    else { ReleaseKey(KeyCode.DownArrow); ReleaseKey(KeyCode.UpArrow); }

                    if (button == 0) { SendKeyPress(KeyCode.Space); } // Trykker på joystick-knappen
                    else { ReleaseKey(KeyCode.Space); }
                }
            }
            catch (TimeoutException) { }
        }
    }

    void SendKeyPress(KeyCode key)
    {
        if (!Input.GetKey(key)) { Input.GetKeyDown(key); } // Simuler tastetryk
    }

    void ReleaseKey(KeyCode key)
    {
        if (Input.GetKey(key)) { Input.GetKeyUp(key); } // Slip tasten
    }

    void OnApplicationQuit()
    {
        serialPort.Close(); // Luk seriel port når Unity lukker
    }
}
