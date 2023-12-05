using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AttackStruggle : BaseAttackScript
{


    public override void ChooseEntities(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity)
    {

    }

    public override void Action(List<GameObject> affectedEntites, BaseEntityScipt currentEntity)
    {
        currentEntity.animator.SetTrigger("Attack");
        for (int i = 0; i < affectedEntites.Count; i++)
        {
            BaseEntityScipt entityScript = affectedEntites[i].GetComponent<BaseEntityScipt>();
            entityScript.healthSystem.DealDamage(2);
        }
    }



    public override void SetVariables(List<GameObject> enemies = null, List<GameObject> friends = null)
    {
        possibleEntitiesToSelect.Clear();
        selectedEntites.Clear();
        energyGenerated = 0;
        energyCost = 1;
        willChooseTargets = true;
        stringName = "Struggle";
        stringAttackDamage = "2";
        stringDebateDamage = "0";
        stringDescription = "Struggle against a specific enemy";
        numOfEntitesToSelect = 1;
        canOnlySelectDifferentTypes = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            possibleEntitiesToSelect.Add(enemies[i]);
        }


        ExtendVariables();
    }
}
