using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void DealDamage(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            KillEntity();
        }
        else
        {
            currentHealth -= damage;
        }
        SetHealthbar();
    }

    public void HealForAmount(float healAmount)
    {
        if (currentHealth + healAmount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healAmount;
        }
        SetHealthbar();
    }

    public void KillEntity()
    {
        currentHealth = 0;
    }

    private void SetHealthbar()
    {
        if(healthBar != null) 
        healthBar.fillAmount = currentHealth / maxHealth;
    }

}
