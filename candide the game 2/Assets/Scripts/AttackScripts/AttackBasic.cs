using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackBasic : BaseAttackScript
{
    public override void Action(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity)
    {
        
        currentEntity.animator.SetTrigger("Attack");
        GameObject enemyToAttack = enemies[Random.Range(0, enemies.Count)]; ;
        BaseEntityScipt entityScript = enemyToAttack.GetComponent<BaseEntityScipt>();
        entityScript.healthSystem.DealDamage(1);
    }

    public override void SetVariables()
    {
        stringName = "Ball slapper";
        stringAttackDamage = "6";
        stringDebateDamage = "0";

        ExtendVariables();
    }
}
