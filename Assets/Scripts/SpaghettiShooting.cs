using System.Collections.Generic;
using UnityEngine;

public class SpaghettiShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<Transform> shootPoints;

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
        foreach(Transform bulletPos in shootPoints){
            Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
        }
    }
}
