using UnityEngine;
using TMPro;
public class Collect : MonoBehaviour
{
    public Transform MotherTransform;
    public bool ObjectCheck;
    public TextMeshProUGUI TrashText;
    public TextMeshProUGUI MailText;
    public TextMeshProUGUI FejlText;

    public bool IsInGame;

    float TrashScore;
    float FejlScore;
    float MailScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MotherTransform.transform.position.y<10){
            IsInGame = true;

        } else{
            IsInGame = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NigerianPrince")&&ObjectCheck==true){ //Skal fange prinsen
            Destroy(other.gameObject);
            TrashScore = TrashScore + 1;
            TrashText.text = " " + TrashScore;

        } else if(other.CompareTag("Collect")&&ObjectCheck==true){ //fanger kat ved fejl
            Destroy(other.gameObject);
            FejlScore = FejlScore + 1;
            FejlText.text = " " + FejlScore;
            
        } else if(other.CompareTag("NigerianPrince")&&ObjectCheck==false){ //fanger prins ved fejl
            Destroy(other.gameObject);
            FejlScore = FejlScore + 1;
            FejlText.text = " " + FejlScore;
        } else if(other.CompareTag("Collect")&&ObjectCheck==false){ //Skal fange kat
            Destroy(other.gameObject);
            MailScore = MailScore + 1;
            MailText.text = " " + MailScore;
        }


    }
    
}
