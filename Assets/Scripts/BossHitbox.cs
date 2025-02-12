using System;
using UnityEditor.UI;
using UnityEngine;

public class BossHitbox : MonoBehaviour
{
    public float health = 100f;
    public GameObject parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void takeDamage(float damage){
        health -= damage;
        if (health <= 0){
            Destroy(parent);
        }
    }
}
