using System;
using UnityEditor.UI;
using UnityEngine;

public class BossHitbox : MonoBehaviour
{
    public float health = 100f;
    public GameObject parent;

    public void takeDamage(float damage){
        health -= damage;
        if (health <= 0){
            Destroy(parent);
        }
    }
}
