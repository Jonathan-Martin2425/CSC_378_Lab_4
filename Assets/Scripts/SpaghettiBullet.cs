using UnityEngine;

public class SpaghettiBullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float bulletSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            Vector3 bulletDir = player.transform.position - transform.position;
            rb.linearVelocity = new Vector2(bulletDir.x, bulletDir.y).normalized * bulletSpeed;

            float bulletRotation = Mathf.Atan2(-bulletDir.y, -bulletDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, bulletRotation + 90);
        }
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
