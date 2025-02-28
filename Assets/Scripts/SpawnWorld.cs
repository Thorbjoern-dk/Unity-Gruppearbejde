using UnityEngine;

public class SpawnWorld : MonoBehaviour
{

    public Vector3 edge1;
    public Vector3 edge2;

    public GameObject NigerPrince;

    public GameObject NigerPrince2;
    public GameObject Kat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i<40; i++){
            if (Random.Range(0,2)==1){
                if(Random.Range(0,2)==1){
                    Instantiate(NigerPrince2, new Vector3(Random.Range(edge1.x, edge2.x), Random.Range(edge1.y, edge2.y), 0), Quaternion.identity);
                } else{
                    Instantiate(NigerPrince, new Vector3(Random.Range(edge1.x, edge2.x), Random.Range(edge1.y, edge2.y), 0), Quaternion.identity);
                }
                
            } else{
                Instantiate(Kat, new Vector3(Random.Range(edge1.x, edge2.x), Random.Range(edge1.y, edge2.y), 0), Quaternion.identity);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
