using UnityEngine;

public class SpaghettiShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletPos;

    private float bulletDelay;
    public float bulletDelayThreshold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletDelay = bulletDelay + Time.deltaTime;

        if (bulletDelay > bulletDelayThreshold)
        {
            bulletDelay = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
    }
}
