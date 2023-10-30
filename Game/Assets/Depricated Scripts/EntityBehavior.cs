using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{
    public GameObject behaviour;
    protected int maxHealth;
    protected int health;
    protected int strength;

    public EntityBehavior(int maxHealth, int strength)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
        this.strength = strength;
    }

    protected void DealDamage(int damageAmount)
    {
        health -= damageAmount;
    }

    protected void HealAmount(int healAmount)
    {
        health += healAmount;
    }

    protected int GetHealth()
    {
        return health;
    }

}
