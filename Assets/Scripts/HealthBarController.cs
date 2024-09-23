using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthSlider;

    public float maxHealth = 100f;
    private float currentHealth;

    public float damagePerSecond = 5f;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        if (currentHealth > 0)
        {
            currentHealth -= damagePerSecond * Time.deltaTime;
            if (currentHealth < 0) currentHealth = 0; 

            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.Log("Здоровье закончилось!");
        }
    }
}

