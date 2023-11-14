using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoralitySystem : MonoBehaviour
{
    public float currentMorality;
    public float maxMorality;

    public Image moralityBar;

    private void Start()
    {
        currentMorality = maxMorality;
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
        SetMoralitybar();
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
        SetMoralitybar();
    }

    public void RetireEnemy()
    {

        currentMorality = 0;
        
    }

    private void SetMoralitybar()
    {
        if(moralityBar != null)
        moralityBar.fillAmount = currentMorality / maxMorality;
    }
}
