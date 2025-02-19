using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHitbox : MonoBehaviour
{
    public float maxHealth = 2000f;
    public float curHealth = 2000f;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    private bool isFlashing;
    [SerializeField] HealthBar healthBar;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(maxHealth, curHealth);
    }

    public void takeDamage(float damage)
    {
        curHealth -= damage;
        healthBar.UpdateHealthBar(maxHealth, curHealth);
        if (!isFlashing)
        {
            StartCoroutine(onDamage());
        }
        if (curHealth <= 0){
            Destroy(gameObject);
            SceneManager.LoadScene("WinScreen");
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
