using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    private bool isShooting = false;
    private bool isShootingRight = false;
    public Transform rightShootingPos;
    public Transform leftShootingPos;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (isShootingRight)
        {
            Instantiate(bulletPrefab, rightShootingPos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(bulletPrefab, leftShootingPos.position, Quaternion.identity);
        }

        isShootingRight = !isShootingRight;
    }

    private void OnAttack(InputValue inputValue)
    {
        // When space bar is pressed, isShooting will be set to the opposite of its current value
        isShooting = !isShooting;
    }
}
