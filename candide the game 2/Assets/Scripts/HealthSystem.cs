using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public BarValueScript healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetBarValue(maxHealth, currentHealth);
            healthBar.SetText(currentHealth, maxHealth);
        }
            
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
        if (healthBar != null)
            healthBar.SetBarValue(maxHealth, currentHealth);
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
        if (healthBar != null)
            healthBar.SetBarValue(maxHealth, currentHealth);
    }

    public void KillEntity()
    {
        currentHealth = 0;
    }



}
