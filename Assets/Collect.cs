using UnityEngine;

public class Collect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("NigerianPrince"))
        {
            Destroy(other.gameObject);
            Debug.Log("Destroy");
        }
    }
    
}
