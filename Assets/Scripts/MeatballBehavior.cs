using System;
using Unity.VisualScripting;
using UnityEngine;

public class MeatballBehavior : MonoBehaviour
{
    private GameObject player;
    public GameObject smallerBall;
    private Rigidbody2D rb;
    public float bulletSpeed = 5;
    public float smallerBallOffset = 0.5f;

    public float fuseTime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 bulletDir = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(bulletDir.x, bulletDir.y).normalized * bulletSpeed;

        float bulletRotation = Mathf.Atan2(-bulletDir.y, -bulletDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, bulletRotation + 90);
        System.Random rng = new System.Random();
        fuseTime -= (float)rng.NextDouble();
    }

    // Update is called once per frame
    void Update()
    {
        fuseTime -= Time.deltaTime;
        if (fuseTime <= 0){
            float x;
            float y;
            for(float i = 0; i < 360; i += 45){
                x = (float)Math.Cos(i * (Math.PI / 180)) * smallerBallOffset;
                y = (float)Math.Sin(i * (Math.PI / 180)) * smallerBallOffset;
                Instantiate(smallerBall, 
            gameObject.transform.position + new Vector3(x, y, 0), 
            Quaternion.Euler(new Vector3(0, 0, i)));
            }
            Destroy(gameObject);
        }
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
