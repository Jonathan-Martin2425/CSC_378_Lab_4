using System;
using UnityEditor.UI;
using UnityEngine;

public class BossHitbox : MonoBehaviour
{
    public float health = 100f;

    public void takeDamage(float damage){
        health -= damage;
        //change color
        if (health <= 0){
            Destroy(gameObject);
        }
    }
}
