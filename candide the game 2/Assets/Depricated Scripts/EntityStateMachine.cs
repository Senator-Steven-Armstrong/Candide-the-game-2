using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityStateMachine : MonoBehaviour
{
    public enum TurnState
    {
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    public Vector3 attackPosition;
    protected Vector3 startPosition;

    private bool actionStarted = false;
    private float animSpeed = 20f;

    protected BattleStateMachine BSM;

    void Start()
    {
        currentState = TurnState.SELECTING;
        startPosition = transform.position;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
    }

    protected IEnumerator TimeForAction()
    {

        if(actionStarted)
        {
            yield break;
        }
        actionStarted = true;

        //Move attacking enemy / player to center of their side of the screen (if there are more than 2 entities in the team)
        while(MoveTowardsPosition(attackPosition))
        {
            yield return null;     
        }

        // wait and play animation
        yield return new WaitForSeconds(1.5f);
        //do damage

        //move back
        while(MoveTowardsPosition(startPosition))
        {
            yield return null;     
        }

        //remove this performer from the list


        //reset BSM -> wait
        BSM.actionList.RemoveAt(0);
        BSM.currentBattleState = BattleStateMachine.BattleStates.WAIT;

        actionStarted = false;
        currentState = TurnState.WAITING;

    }

    protected bool MoveTowardsPosition(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

}
