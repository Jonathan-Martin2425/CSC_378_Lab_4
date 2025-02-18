using System;
using Unity.Collections;
using UnityEngine;

public class RotatingSauceBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed;

    public float rotationSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, 0, rotationSpeed*Time.deltaTime));
        float x = (float)Math.Cos(transform.eulerAngles.z * (Math.PI / 180)) * bulletSpeed;
        float y = (float)Math.Sin(transform.eulerAngles.z * (Math.PI / 180)) * bulletSpeed;
        rb.linearVelocity = new Vector2(x, y);
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
