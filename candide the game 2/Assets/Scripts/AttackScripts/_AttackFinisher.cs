using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AttackFinisher : BaseAttackScript
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

            int damage = Mathf.RoundToInt((1 - entityScript.moralitySystem.currentMorality / entityScript.moralitySystem.maxMorality) * 18);

            entityScript.healthSystem.DealDamage(damage);
            entityScript.moralitySystem.DealDamage(2);
        }
    }

    public override void SetVariables(List<GameObject> enemies = null, List<GameObject> friends = null)
    {
        possibleEntitiesToSelect.Clear();
        selectedEntites.Clear();
        energyGenerated = 0;
        energyCost = 3;
        willChooseTargets = true;
        stringName = "Finisher";
        stringAttackDamage = "0-18";
        stringDebateDamage = "2";
        stringDescription = "Destroy your enemy depeniding on their remaining morality!";
        numOfEntitesToSelect = 1;
        canOnlySelectDifferentTypes = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            possibleEntitiesToSelect.Add(enemies[i]);
        }


        ExtendVariables();
    }
}
