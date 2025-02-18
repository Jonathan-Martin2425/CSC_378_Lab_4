using System;
using Unity.Mathematics;
using UnityEngine;

public class RotateFiring : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float SauceBulletDelay = 0.4f;
    public float rotationSpeed = 0f;
    private float bulletsOffset = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float x;
        float y;
        for(float i = 0; i < 360; i += 36)
        {
            x = (float)Math.Cos(i * (Math.PI / 180)) * bulletsOffset;
            y = (float)Math.Sin(i * (Math.PI / 180)) * bulletsOffset;
            Instantiate(bulletPrefab, 
            gameObject.transform.position + new Vector3(x, y, 0), 
            Quaternion.Euler(new Vector3(0, 0, i)));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
