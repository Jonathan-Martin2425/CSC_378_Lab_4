using System;
using System.Collections;
using UnityEngine;

public class BossP2Controller : MonoBehaviour
{
    // private float attackTimer = 0f;
    [SerializeField] private float health = 100f;
    [SerializeField] private GameObject meatP2;
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int numBullets = 10;
    private float bulletsOffset = 0.5f;
    private int pulseCount = 0;
    private int pulseLimit = 5;
    private String attackType = "MeatBounce";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn = transform.Find("Spawn").gameObject;

        StartCoroutine(AttackLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AttackLoop()
    {
        float meatTimer = 5f;
        float pulseDelay;

        // Exit loop when boss has died
        while (health > 0) 
        {
            if (attackType == "MeatBounce")
            {
                GameObject[] meats = {MeatBounce(new Vector2(-5f, 7f)), MeatBounce(new Vector2(5f, 7f))};
                yield return new WaitForSeconds(meatTimer);
                DestroyObjects(meats);
                attackType = "SaucePulse";
            }
            else if (attackType == "SaucePulse")
            {
                while (pulseCount < pulseLimit)
                {
                    pulseDelay = UnityEngine.Random.Range(0f, 2f);
                    SpawnBullets();
                    pulseCount += 1;
                    yield return new WaitForSeconds(pulseDelay);
                }
                pulseCount = 0;
                attackType = "MeatBounce";
            }
        }
    }

    GameObject MeatBounce(Vector2 force)
    {
        GameObject obj = Instantiate(meatP2, spawn.transform.position, spawn.transform.rotation);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        rb.AddForce(force, ForceMode2D.Impulse);

        return obj;
    }

    void SpawnBullets()
    {
        float x;
        float y;
        int bulletSeparation = 360 / numBullets;
        for(float i = 0; i < 360; i += bulletSeparation)
        {
            x = (float)Math.Cos(i * (Math.PI / 180)) * bulletsOffset;
            y = (float)Math.Sin(i * (Math.PI / 180)) * bulletsOffset;
            Instantiate(bulletPrefab, 
            gameObject.transform.position + new Vector3(x, y, 0), 
            Quaternion.Euler(new Vector3(0, 0, i)));
        }
    }

    void DestroyObjects(GameObject[] objs)
    {
        foreach (var obj in objs)
        {
            Destroy(obj);
        }
    }
}
