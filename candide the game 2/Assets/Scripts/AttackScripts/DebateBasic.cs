using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebateBasic : BaseDebateScript
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
        currentEntity.animator.SetTrigger("Debate");
        for (int i = 0; i < affectedEntites.Count; i++)
        {
            BaseEntityScipt entityScript = affectedEntites[i].GetComponent<BaseEntityScipt>();
            entityScript.moralitySystem.DealDamage(2);
        }
    }
      
    

    public override void SetVariables(List<GameObject> enemies = null, List<GameObject> friends = null)
    {
        energyGenerated = 1;
        energyCost = 0;
        stringName = "Candide balls";
        stringAttackDamage = "0";
        stringDebateDamage = "10";
        stringDescription = "so basically you die";

        ExtendVariables();
    }
}
