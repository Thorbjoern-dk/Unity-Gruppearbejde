using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DestroyMyself : MonoBehaviour
{

    public TextMeshProUGUI text;

    public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-10){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Ground")){
            rb.AddForce(new Vector3(0,Random.Range(300,500),0));
            Debug.Log("addForce");
        }

    }
    
}
