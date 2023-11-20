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
            currentEntity.attackScripts[index].Action(enemies, friends, currentEntity);
        }
        else
        {
            //DEBATE
            int index = Random.Range(0, currentEntity.debateScripts.Count);
            currentEntity.debateScripts[index].Action(enemies, friends, currentEntity);
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

    //public override IEnumerator Action(List<GameObject> enemies, List<GameObject> friends)
    //{
    //    Debug.Log("bulgar????");

    //    currentMoveState = MoveStates.MOVETOBATTLE;
    //    yield return new WaitForSeconds(moveTime);

    //    currentMoveState = MoveStates.WAIT;

    //    GameObject enemyToAttack = ChooseRandomEntity(friends);
    //    BaseEntityScipt entityScript = enemyToAttack.GetComponent<BaseEntityScipt>();
    //    entityScript.healthSystem.DealDamage(Random.Range(1, 2));
    //    animator.SetTrigger("Attack");

    //    yield return new WaitForSeconds(1);

    //    currentMoveState = MoveStates.MOVETOSTART;
    //    yield return new WaitForSeconds(moveTime);
    //    currentMoveState = MoveStates.WAIT;
    //}
}
