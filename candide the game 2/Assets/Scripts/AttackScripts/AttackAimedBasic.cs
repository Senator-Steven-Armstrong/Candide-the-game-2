using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAimedBasic : BaseAttackScript
{
    public override void Action(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity)
    {
        //yield return WaitForInput();

        currentEntity.animator.SetTrigger("Attack");
        GameObject enemyToAttack = enemies[Random.Range(0, enemies.Count)]; ;
        BaseEntityScipt entityScript = enemyToAttack.GetComponent<BaseEntityScipt>();
        entityScript.healthSystem.DealDamage(Random.Range(0, 3));
    }

    public override void SetVariables()
    {
        willChooseTargets = true;
        stringName = "Gun";
        stringAttackDamage = "0-3";
        stringDebateDamage = "0";
        stringDescription = "Literally just shoot you enemy";

        ExtendVariables();
    }
}
