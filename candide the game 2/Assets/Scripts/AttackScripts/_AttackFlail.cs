using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AttackFlail : BaseAttackScript
{
    public override void ChooseEntities(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity)
    {
        List<GameObject> entities = new List<GameObject>
        {
            enemies[Random.Range(0, enemies.Count)]
        };
        Action(entities, currentEntity);


    }

    public override void Action(List<GameObject> affectedEntites, BaseEntityScipt currentEntity)
    {
        currentEntity.animator.SetTrigger("Attack");
        for (int i = 0; i < affectedEntites.Count; i++)
        {
            BaseEntityScipt entityScript = affectedEntites[i].GetComponent<BaseEntityScipt>();
            entityScript.healthSystem.DealDamage(Random.Range(0, 1));
        }
    }

    public override void SetVariables(List<GameObject> enemies = null, List<GameObject> friends = null)
    {
        energyGenerated = 1;
        energyCost = 0;
        stringName = "Flail";
        stringAttackDamage = "1-2";
        stringDebateDamage = "0";
        stringDescription = "Flail around your arms in random directions";

        ExtendVariables();
    }
}
