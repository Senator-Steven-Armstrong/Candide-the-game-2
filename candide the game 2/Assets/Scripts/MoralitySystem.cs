using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoralitySystem : MonoBehaviour
{
    public float currentMorality;
    public float maxMorality;

    public BarValueScript moralityBar;

    private void Start()
    {
        currentMorality = maxMorality;
        if(moralityBar != null)
        {
            moralityBar.SetBarValue(maxMorality, currentMorality);
            moralityBar.SetText(currentMorality, maxMorality);
        }
            
    }

    public void DealDamage(float damage)
    {
        if (currentMorality - damage <= 0)
        {
            RetireEnemy();
        }
        else
        {
            currentMorality -= damage;
        }
        if (moralityBar != null)
            moralityBar.SetBarValue(maxMorality, currentMorality);
    }

    public void HealForAmount(float healAmount)
    {
        if (currentMorality + healAmount >= maxMorality)
        {
            currentMorality = maxMorality;
        }
        else
        {
            currentMorality += healAmount;
        }
        if (moralityBar != null)
            moralityBar.SetBarValue(maxMorality, currentMorality);
    }

    public void RetireEnemy()
    {

        currentMorality = 0;
        
    }
}
