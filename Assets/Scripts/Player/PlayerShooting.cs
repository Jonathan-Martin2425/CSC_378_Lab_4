using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject leftBulletPrefab;
    public GameObject rightBulletPrefab;
    private bool isShooting = false;
    private bool isShootingRight = true;
    public Transform rightShootingPos;
    public Transform leftShootingPos;
    public float fireRate = 1f;
    private Animator playerAnimator;
    private float nextFireTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        //nextFireTime = fireRate;
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
            Instantiate(rightBulletPrefab, rightShootingPos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(leftBulletPrefab, leftShootingPos.position, Quaternion.identity);
        }

        isShootingRight = !isShootingRight;
    }

    private void OnAttack(InputValue inputValue)
    {
        // When space bar is pressed, isShooting will be set to the opposite of its current value
        isShooting = !isShooting;
        playerAnimator.SetBool("isShooting", isShooting);
    }
}
