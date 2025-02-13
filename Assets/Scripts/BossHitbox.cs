using System;
using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class BossHitbox : MonoBehaviour
{
    public float health = 100f;
    public GameObject parent;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    private bool isFlashing;

    void Start()
    {
        spriteRenderer = parent.GetComponent<SpriteRenderer>();
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        
        if (!isFlashing)
        {
            StartCoroutine(onDamage());
        }
        
        if (health <= 0)
        {
            Destroy(parent);
        }
    }

    private IEnumerator onDamage()
    {
        isFlashing = true;
        Color color = spriteRenderer.color;

        spriteRenderer.color = new Color(
            color.r * 1.5f,
            color.g * 1.5f,
            color.b * 1.5f,
            color.a
        );

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = color;
        isFlashing = false;
    }
}
