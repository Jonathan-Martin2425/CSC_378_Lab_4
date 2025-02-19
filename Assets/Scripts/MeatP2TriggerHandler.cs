using UnityEngine;

public class MeatP2TriggerHandler : MonoBehaviour
{
    public int maxBounces = 3;
    private int numBounces = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D obj){
        if(obj.tag == "Player"){
            obj.GetComponent<PlayerHealth>().takeDamage();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.tag == "Player"){
            obj.gameObject.GetComponent<PlayerHealth>().takeDamage();
            Destroy(gameObject);
        }else{
            numBounces += 1;
            if(numBounces >= maxBounces){
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionStay2D(Collision2D obj)
    {
        if(obj.gameObject.tag == "Player"){
            obj.gameObject.GetComponent<PlayerHealth>().takeDamage();
            Destroy(gameObject);
        }else{
            numBounces += 1;
            if(numBounces >= maxBounces){
                Destroy(gameObject);
            }
        }
    }
}
