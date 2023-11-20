using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityScipt : MonoBehaviour
{
    public Animator animator;
    public HealthSystem healthSystem;
    public MoralitySystem moralitySystem;
    public EnergySystem energySystem;
    [NonSerialized]public Vector3 startPosition;
    [NonSerialized] public Vector3 battlePosition;
    public float moveTime;
    public int initiative;
    public List<BaseAttackScript> attackScripts = new();
    public List<BaseDebateScript> debateScripts = new();
    public bool isPlayerControlled;

    public CameraBehaviourScript cameraBehaviourScript;

    public float elapsedTime;

    public BaseAttackScript attack1;

    public enum MoveStates
    {
        WAIT,
        MOVETOBATTLE,
        MOVETOSTART
    }

    protected MoveStates currentMoveState;

    public void SetVariables()
    {
        if (animator == null)
            animator = gameObject.GetComponentInChildren<Animator>();
        if (healthSystem == null)
            healthSystem = gameObject.GetComponent<HealthSystem>();
        if (moralitySystem == null)
            moralitySystem = gameObject.GetComponent<MoralitySystem>();
        if (energySystem == null)
            energySystem = gameObject.GetComponent<EnergySystem>();
        if (cameraBehaviourScript == null)
            cameraBehaviourScript = Camera.main.GetComponent<CameraBehaviourScript>();
        startPosition = transform.position;
    }

    public IEnumerator MoveToBattle()
    {
        currentMoveState = MoveStates.MOVETOBATTLE;
        if (isPlayerControlled)
        {
            Debug.Log(cameraBehaviourScript);
            StartCoroutine(cameraBehaviourScript.MoveToBattlePos(moveTime));
        }

        yield return new WaitForSeconds(moveTime);

        currentMoveState = MoveStates.WAIT;
    }

    public IEnumerator MoveFromBattle()
    {
        yield return new WaitForSeconds(1);

        currentMoveState = MoveStates.MOVETOSTART;
        if (isPlayerControlled)
        {
            StartCoroutine(cameraBehaviourScript.MoveToOverviewPos(moveTime));
        }
        yield return new WaitForSeconds(moveTime);
        currentMoveState = MoveStates.WAIT;
    }


    public void MoveIntoPosition(Vector3 from, Vector3 to)
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / moveTime;
        gameObject.transform.position = Vector3.Lerp(from, to, percentageComplete);
    }

    protected void MovePositionsChecker()
    {
        switch (currentMoveState)
        {
            case (MoveStates.WAIT):
                elapsedTime = 0;
                break;
            case (MoveStates.MOVETOSTART):
                MoveIntoPosition(startPosition, battlePosition);
                break;
            case (MoveStates.MOVETOBATTLE):
                MoveIntoPosition(battlePosition, startPosition);
                break;
        }
    }

    public abstract void AiChooseMove(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity);


}
