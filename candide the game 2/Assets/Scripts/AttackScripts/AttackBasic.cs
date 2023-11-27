using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackBasic : BaseAttackScript
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
            entityScript.healthSystem.DealDamage(1);
        }
    }



    public override void SetVariables(List<GameObject> enemies = null, List<GameObject> friends = null)
    {
        stringName = "Ball slapper";
        stringAttackDamage = "6";
        stringDebateDamage = "0";
        stringDescription = "Slap around your enemies balls";

        ExtendVariables();
    }
}
