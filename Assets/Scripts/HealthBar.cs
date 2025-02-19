using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealthBar(float maxHealth, float curHealth) {
        healthSlider.value = curHealth / maxHealth;
    }
}
