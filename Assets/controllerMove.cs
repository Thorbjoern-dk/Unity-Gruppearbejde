using System;
using System.IO.Ports;
using UnityEngine;
public class controllerMove : MonoBehaviour
{
    SerialPort serialPort; // = new SerialPort("COM4", 115200); // ⚠️ Udskift med din COM-port

    void Start()
    {
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

                if (data == "LEFT") { SimulateKeyPress(KeyCode.LeftArrow); }
                if (data == "RIGHT") { SimulateKeyPress(KeyCode.RightArrow); }
                if (data == "UP") { SimulateKeyPress(KeyCode.UpArrow); }
                if (data == "DOWN") { SimulateKeyPress(KeyCode.DownArrow); }
            }
            catch (TimeoutException) { }
        }
    }

    void SimulateKeyPress(KeyCode key)
    {
        if (!Input.GetKey(key)) { Input.GetKeyDown(key); } // Simuler tastetryk
    }

    void OnApplicationQuit()
    {
        serialPort.Close();
    }
}