using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityScipt : MonoBehaviour
{
    public Animator animator;
    public HealthSystem healthSystem;
    public MoralitySystem moralitySystem;
    public EnergySystem energySystem;
    public Vector3 startPosition;
    public Vector3 battlePosition;
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
        startPosition = transform.position;
    }

    public IEnumerator MoveToBattle()
    {
        currentMoveState = MoveStates.MOVETOBATTLE;
        if (isPlayerControlled)
        {
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
    public GameObject ChooseRandomEntity(List<GameObject> entites)
    {
        int entityIndex = Random.Range(0, entites.Count);
        return entites[entityIndex];
    }

    public void MoveIntoBattlePos()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / moveTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, battlePosition, percentageComplete);
    }

    public void MoveBackToStartPos()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / moveTime;
        gameObject.transform.position = Vector3.Lerp(battlePosition, startPosition, percentageComplete);
    }

    protected void MovePositionsChecker()
    {
        switch (currentMoveState)
        {
            case (MoveStates.WAIT):
                elapsedTime = 0;
                break;
            case (MoveStates.MOVETOSTART):
                MoveBackToStartPos();
                break;
            case (MoveStates.MOVETOBATTLE):
                MoveIntoBattlePos();
                break;
        }
    }

    


}
