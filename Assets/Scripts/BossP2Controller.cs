using System.Collections;
using UnityEngine;

public class BossP2Controller : MonoBehaviour
{
    // private float attackTimer = 0f;
    [SerializeField] private float health = 100f;
    [SerializeField] private GameObject meatP2;
    [SerializeField] private GameObject spawn;
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
        float attackTime = 10f;

        // Exit loop when boss has died
        while (health > 0) 
        {
            GameObject[] meats = {MeatBounce(new Vector2(-5f, 7f)), MeatBounce(new Vector2(5f, 7f))};

            yield return new WaitForSeconds(attackTime);

            DestroyObjects(meats);

            // Simulate some other attack...
            yield return new WaitForSeconds(attackTime);
        }
    }

    GameObject MeatBounce(Vector2 force)
    {
        GameObject obj = Instantiate(meatP2, spawn.transform.position, spawn.transform.rotation);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        rb.AddForce(force, ForceMode2D.Impulse);

        return obj;
    }

    void DestroyObjects(GameObject[] objs)
    {
        foreach (var obj in objs)
        {
            Destroy(obj);
        }
    }
}
