using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public float damage = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        
        
    }

    void OnTriggerEnter2D(Collider2D obj){
        if(obj.tag == "Boss"){
            obj.GetComponent<BossHitbox>().takeDamage(damage);
            Destroy(gameObject);
        }
        if(obj.tag == "Offscreen"){
            Destroy(gameObject);
        }
    }


}
