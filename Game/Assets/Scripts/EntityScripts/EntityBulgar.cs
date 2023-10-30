using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBulgar : BaseEntityScipt
{
    private void Start()
    {
        SetVariables();
    }

    private void Update()
    {
        MovePositionsChecker();
    }

    public override IEnumerator Action(List<GameObject> enemies, List<GameObject> friends)
    {
        Debug.Log("bulgar????");

        currentMoveState = MoveStates.MOVETOBATTLE;
        yield return new WaitForSeconds(moveTime);

        currentMoveState = MoveStates.WAIT;

        GameObject enemyToAttack = ChooseRandomEntity(friends);
        BaseEntityScipt entityScript = enemyToAttack.GetComponent<BaseEntityScipt>();
        entityScript.healthSystem.DealDamage(Random.Range(1, 2));
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1);

        currentMoveState = MoveStates.MOVETOSTART;
        yield return new WaitForSeconds(moveTime);
    }
}
