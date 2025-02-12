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

    // Update is called once per frame
    void Update()
    {
        if(tr.position.x >= 4.6){
            rb.linearVelocityX = -bossSpeed;
        }else if(tr.position.x <= -4.6){
            rb.linearVelocityX = bossSpeed;
        }
        tr.Rotate(new Vector3(0, 0, 45 * Time.deltaTime));
    }
}
