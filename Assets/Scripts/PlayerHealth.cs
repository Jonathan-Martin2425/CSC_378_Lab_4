using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float lives = 3;
    public float iFrames = 3;
    private float curIframes = 0;
    private SpriteRenderer spr;
    public float blinkingSpeed = 0.25f;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(curIframes > 0){
            curIframes -= Time.deltaTime;
            spr.forceRenderingOff = (curIframes / blinkingSpeed % 2) >= 1;
        }else{
            spr.forceRenderingOff = false;
        }
    }

    public void takeDamage(){
        if(curIframes <= 0){
            curIframes = iFrames;
            lives -= 1;
            if(lives <= 0){
                Destroy(gameObject);
            }
        }
    }
}
