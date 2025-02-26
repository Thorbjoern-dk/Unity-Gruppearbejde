using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.IO.Ports;

public class BasketControllerMovement : MonoBehaviour
{

    public Transform TrashBin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(TrashBin.position.y<0){
            transform.position = new Vector3(TrashBin.position.x, 20, transform.position.z);
        } else{
            transform.position = new Vector3(TrashBin.position.x, -3, transform.position.z);
        }
    }
}
