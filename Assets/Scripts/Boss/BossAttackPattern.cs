using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BossAttackPattern : MonoBehaviour
{
    public List<Transform> bulletShootPoints;
    public GameObject meatball;
    public GameObject sauceBullet;
    public GameObject bouncingMeatball;
    public GameObject spinMeatball;
    public List<Transform> tentacleShootPoints;
    public List<SpriteRenderer> tentaclesArms;
    public GameObject indicator;
    public GameObject lazer;
    public float indicatorTime = 2f;
    public float bulletsOffset = 0.5f;
    public float SauceBulletDelay = 0.4f;
    public float MeatballBulletDelay = 2f;
    public float BounceBulletDelay = 3f;
    public float pulseDelay = 0.5f;
    public int numSauceBullets = 10;
    public int numMeatballBullets = 3;
    public int numBounceBullets = 3;
    public int pulseLimit = 5;
    public int pulseAmmount = 10;
    private string attackType = "Spaghetti";
    private Rigidbody2D rb;
    private List<Tuple<Vector3, Quaternion>> indicatedPositions;
    private Animator bossAnimator;
    private int phase;
    private float bossSpeed;
    public int bulletsShot = 0;
    private float attackTimer = 0f;
    //private String attackType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossSpeed = GetComponent<BossMovement>().bossSpeed;
        rb = GetComponent<Rigidbody2D>();
        bossAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        phase = bossAnimator.GetInteger("phase");
        if(attackTimer >= 0){
            attackTimer -= Time.deltaTime;
        }else if(phase == 1){
            if(attackType == "Spaghetti"){
                Shoot(sauceBullet);
                bulletsShot++;
                if(bulletsShot >= numSauceBullets){
                    attackType = "Meatball";
                    attackTimer = MeatballBulletDelay;
                    bulletsShot = 0;
                }else{
                    attackTimer = SauceBulletDelay;
                }
            }else if(attackType == "Meatball"){
                Shoot(meatball);
                bulletsShot++;
                if(bulletsShot >= numMeatballBullets){
                    attackType = "Lazer1";
                    bulletsShot = 0;
                }
                attackTimer = MeatballBulletDelay;
            }
            else if(attackType == "Lazer1"){
                indicatedPositions = StartLazerAttack();
            }else if (attackType == "Lazer2"){
                for(int i = 0; i < 2; i++){
                    Instantiate(lazer, indicatedPositions[i].Item1, indicatedPositions[i].Item2);
                }
                foreach(SpriteRenderer arm in tentaclesArms){
                    arm.forceRenderingOff = true;
                }
                attackTimer = indicatorTime;
                attackType = "Lazer3";
            }else if(attackType == "Lazer3"){
                foreach(SpriteRenderer arm in tentaclesArms){
                    arm.forceRenderingOff = false;
                }
                bossAnimator.SetBool("isStretching", false);
                rb.linearVelocityX = bossSpeed;
                attackType = "Spaghetti";   
            }
        }else if(phase == 2){
            if(attackType == "Meatball"){
                Shoot(meatball);
                bulletsShot++;
                if(bulletsShot >= numMeatballBullets){
                    attackType = "MeatBounce";
                    bulletsShot = 0;
                }
                attackTimer = MeatballBulletDelay;
            }else if (attackType == "MeatBounce")
            {
                float force = (float)UnityEngine.Random.Range(-5, -7.5f);
                MeatBounce(new Vector2(force, 7f));
                bulletsShot++;
                if(bulletsShot >= numBounceBullets){
                    attackType = "SaucePulse";
                    bulletsShot = 0;
                }
                attackTimer = BounceBulletDelay;
            }
            else if (attackType == "SaucePulse")
            {
                bossAnimator.SetBool("isSpinning", true);
                SpawnBullets();
                bulletsShot++;
                attackTimer = pulseDelay;
                if(bulletsShot >= pulseLimit){
                    bulletsShot = 0;
                    attackType = "Meatball";
                    attackTimer = MeatballBulletDelay;
                    bossAnimator.SetBool("isSpinning", false);
                }else{
                    attackTimer = pulseDelay;
                }
            }else{
                attackType = "Meatball";
            }
        }
    }

    void Shoot(GameObject bulletPrefab)
    {
        foreach(Transform bulletPos in bulletShootPoints){
            Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
        }
    }

    public List<Tuple<Vector3, Quaternion>> StartLazerAttack(){
        rb.linearVelocityX = 0;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        List<Tuple<Vector3, Quaternion>> res = new List<Tuple<Vector3, Quaternion>> ();
        foreach(Transform p in tentacleShootPoints){
            Vector3 bulletDir = player.transform.position - p.position;
            float bulletRotation = Mathf.Atan2(-bulletDir.y, -bulletDir.x) * Mathf.Rad2Deg;
            float x = -10 * (float)Math.Cos(Mathf.Deg2Rad * bulletRotation);
            float y = -10 * (float)Math.Sin(Mathf.Deg2Rad * bulletRotation);
            Instantiate(indicator, p.position + new Vector3(x, y, 0), Quaternion.Euler(0, 0, bulletRotation + 90));
            res.Add(new Tuple<Vector3, Quaternion> (p.position + new Vector3(x, y, 0), Quaternion.Euler(0, 0, bulletRotation + 90)));
        }
        attackTimer = indicatorTime;
        attackType = "Lazer2";
        bossAnimator.SetBool("isStretching", true);
        return res;
    }

    void MeatBounce(Vector2 force)
    {
        foreach(Transform bulletPos in tentacleShootPoints){
            GameObject obj = Instantiate(bouncingMeatball, bulletPos.position, Quaternion.identity);

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.AddForce(force, ForceMode2D.Impulse);
            force = new Vector2(-force.x, force.y);
        }
    }

    void SpawnBullets()
    {
        float x;
        float y;
        int bulletSeparation = 360 / pulseAmmount;
        for(float i = 0; i < 360; i += bulletSeparation)
        {
            x = (float)Math.Cos(i * (Math.PI / 180)) * bulletsOffset;
            y = (float)Math.Sin(i * (Math.PI / 180)) * bulletsOffset;
            Instantiate(spinMeatball, 
            gameObject.transform.position + new Vector3(x, y, 0), 
            Quaternion.Euler(new Vector3(0, 0, i)));
        }
    }
}
