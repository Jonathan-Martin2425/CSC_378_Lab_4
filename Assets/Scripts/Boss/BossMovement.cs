using System;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;
    public float bossSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityX = bossSpeed;
        tr = GetComponent<Transform>();
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        Debug.Log(obj.gameObject.tag);
        if(rb.linearVelocityX < 0){
            rb.linearVelocityX = -bossSpeed;
        }else{
            rb.linearVelocityX = bossSpeed;
        }
    }

    void OnCollisionStay2D(Collision2D obj)
    {
        
        Debug.Log(obj.gameObject.tag);
        if(rb.linearVelocityX < 0){
            rb.linearVelocityX = -bossSpeed;
        }else{
            rb.linearVelocityX = bossSpeed;
        }
    }
}
