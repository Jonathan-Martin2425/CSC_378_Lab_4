using System;
using UnityEngine;

public class MeatballPieceBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float angle = transform.eulerAngles.z;
        float x = (float)Math.Cos(angle * (Math.PI / 180));
        float y = (float)Math.Sin(angle * (Math.PI / 180));
        rb.linearVelocity = new Vector2(x, y).normalized * bulletSpeed;
        transform.Rotate(new Vector3(0, 0, -90));
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
        if(obj.tag == "Offscreen"){
            Destroy(gameObject);
        }
    }
}
