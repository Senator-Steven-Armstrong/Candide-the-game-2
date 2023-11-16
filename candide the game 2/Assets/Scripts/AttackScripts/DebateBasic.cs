using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebateBasic : BaseDebateScript
{
    public override void Action(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity)
    {
        currentEntity.animator.SetTrigger("Debate");
        GameObject enemyToAttack = enemies[Random.Range(0, enemies.Count)]; ;
        BaseEntityScipt entityScript = enemyToAttack.GetComponent<BaseEntityScipt>();
        entityScript.moralitySystem.DealDamage(1);
    }

    public override void SetVariables()
    {
        stringName = "Candide balls";
        stringAttackDamage = "0";
        stringDebateDamage = "10";

        ExtendVariables();
    }
}
