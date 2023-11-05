using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EntityCandide : BaseEntityScipt
{
    private int chosenAttackIndex;
    public CameraBehaviourScript cameraBehaviourScript;
    private void Start()
    {
        cameraBehaviourScript = Camera.main.GetComponent<CameraBehaviourScript>();
        SetVariables();
    }

    private void Update()
    {
        MovePositionsChecker();
    }

    public override IEnumerator Action(List<GameObject> enemies, List<GameObject> friends)
    {
        Debug.Log("candide balls?");

        currentMoveState = MoveStates.MOVETOBATTLE;
        StartCoroutine(cameraBehaviourScript.MoveToBattlePos(moveTime));
        yield return new WaitForSeconds(moveTime);

        currentMoveState = MoveStates.WAIT;

        // choose a move
        yield return waitForKeyPress();

        if(chosenAttackIndex == 1)
        {
            Attack1(enemies);
        }
        else if(chosenAttackIndex == 2)
        {
            Attack2(enemies);
        }

        yield return new WaitForSeconds(1);

        currentMoveState = MoveStates.MOVETOSTART;
        StartCoroutine(cameraBehaviourScript.MoveToOverviewPos(moveTime));
        yield return new WaitForSeconds(moveTime);
        currentMoveState = MoveStates.WAIT;
    }

    private void Attack1(List<GameObject> entites)
    {
        GameObject enemyToAttack = ChooseRandomEntity(entites);
        BaseEntityScipt entityScript = enemyToAttack.GetComponent<BaseEntityScipt>();
        entityScript.healthSystem.DealDamage(1);
        animator.SetTrigger("Attack");
    }
    private void Attack2(List<GameObject> entites)
    {
        GameObject enemyToAttack = ChooseRandomEntity(entites);
        BaseEntityScipt entityScript = enemyToAttack.GetComponent<BaseEntityScipt>();
        entityScript.moralitySystem.DealDamage(Random.Range(2, 4));
        animator.SetTrigger("Debate");
    }

    private IEnumerator waitForKeyPress()
    {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                chosenAttackIndex = 1;
                done = true; // breaks the loop
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                chosenAttackIndex = 2;
                done = true; // breaks the loop
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }


        // now this function returns
    }

}
