using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackAimedBasic : BaseAttackScript
{

    public override void ChooseEntities(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity)
    {
        // här vette fan vad som ska hända
    }

    public override void Action(List<GameObject> affectedEntites, BaseEntityScipt currentEntity)
    {
        currentEntity.animator.SetTrigger("Attack");
        for (int i = 0; i < affectedEntites.Count; i++)
        {
            BaseEntityScipt entityScript = affectedEntites[i].GetComponent<BaseEntityScipt>();
            entityScript.healthSystem.DealDamage(1);
        }
    }

    public override void SetVariables(List<GameObject> enemies = null, List<GameObject> friends = null)
    {
        possibleEntitiesToSelect.Clear();
        selectedEntites.Clear();

        willChooseTargets = true;
        stringName = "Gun";
        stringAttackDamage = "0-3";
        stringDebateDamage = "0";
        stringDescription = "Literally just shoot your enemy";
        numOfEntitesToSelect = 2;
        canOnlySelectDifferentTypes = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            possibleEntitiesToSelect.Add(enemies[i]);
        }


        ExtendVariables();
    }
}
