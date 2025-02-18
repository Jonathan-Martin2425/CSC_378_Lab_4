using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float lives = 3;
    public float iFrames = 3;
    private float curIframes = 0;
    public List<SpriteRenderer> sprs;
    public float blinkingSpeed = 0.25f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(curIframes > 0){
            curIframes -= Time.deltaTime;
            foreach(SpriteRenderer spr in sprs){
                spr.forceRenderingOff = (curIframes / blinkingSpeed % 2) >= 1;
            }
        }else{
            foreach(SpriteRenderer spr in sprs){
                spr.forceRenderingOff = false;
            }
        }
    }

    public void takeDamage(){
        if(curIframes <= 0){
            curIframes = iFrames;
            lives -= 1;
            if(lives <= 0){
                Destroy(gameObject);
                SceneManager.LoadScene("LoseScreen");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D obj){
        if(obj.tag == "Boss" || obj.tag == "BossPart"){
            takeDamage();
        }
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.tag == "Boss" || obj.gameObject.tag == "BossPart"){
            takeDamage();
        }
    }

    void OnCollisionStay2D(Collision2D obj)
    {
        if(obj.gameObject.tag == "Boss" || obj.gameObject.tag == "BossPart"){
            takeDamage();
        }
    }
}
