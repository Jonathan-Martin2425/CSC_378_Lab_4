using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHitbox : MonoBehaviour
{
    public float phase2Threshhold = 1500f;
    public float phase2PositionOffset = 0.5f;
    public List<GameObject> nonPhase2Objs;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    private bool isFlashing;
    private Animator bossAnimator;
    public float maxHealth = 2000f;
    public float curHealth;
    public HealthBar healthBar;

    void Start()
    {
        curHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossAnimator = GetComponent<Animator>();
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
        }else if (curHealth <= phase2Threshhold && 
        -phase2PositionOffset < gameObject.transform.position.x
         && gameObject.transform.position.x < phase2PositionOffset){
            bossAnimator.SetInteger("phase", 2);
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = true;
            foreach(GameObject obj in nonPhase2Objs){
                obj.SetActive(false);
            }
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
