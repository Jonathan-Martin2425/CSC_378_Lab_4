using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float lives = 3;
    public float iFrames = 3;
    private float curIframes = 0;

    // Update is called once per frame
    void Update()
    {
        if(curIframes > 0){
            curIframes -= Time.deltaTime;
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
