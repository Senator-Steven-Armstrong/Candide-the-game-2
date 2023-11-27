using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBulgar : BaseEntityScipt
{
    public override void AiChooseMove(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity)
    {

        if(Random.Range(0, 2) == 0)
        {
            //ATTACK
            int index = Random.Range(0, currentEntity.attackScripts.Count);
            currentEntity.attackScripts[index].ChooseEntities(enemies, friends, currentEntity);
        }
        else
        {
            //DEBATE
            int index = Random.Range(0, currentEntity.debateScripts.Count);
            currentEntity.debateScripts[index].ChooseEntities(enemies, friends, currentEntity);
        }

        
    }

    private void Start()
    {
        SetVariables();
        attackScripts.Add(new AttackBasic());
        attackScripts.Add(new AttackBasic());
        debateScripts.Add(new DebateBasic());
    }

    private void Update()
    {
        MovePositionsChecker();
    }

}
