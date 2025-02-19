using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaghettiShooting : MonoBehaviour
{
    public List<Transform> bulletShootPoints;
    public GameObject meatball;
    public GameObject sauceBullet;
    public List<Transform> tentacleShootPoints;
    public List<SpriteRenderer> tentaclesArms;
    public GameObject indicator;
    public GameObject lazer;
    public float indicatorTime = 2f;
    private float attackTimer = 0f;
    public float SauceBulletDelay = 0.4f;
    public float MeatballBulletDelay = 2f;
    public int numSauceBullets = 10;
    public int numMeatballBullets = 3;
    private String attackType = "Spaghetti";
    private Rigidbody2D rb;
    private float bossSpeed;
    private List<Tuple<Vector3, Quaternion>> indicatedPositions;
    private int bulletsShot = 0;
    private Animator bossAnimator;
    private int phase;
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
                    attackType = "Lazer1";
                    bulletsShot = 0;
                }
                attackTimer = MeatballBulletDelay;
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
}
