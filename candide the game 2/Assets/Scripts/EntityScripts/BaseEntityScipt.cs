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

    public float elapsedTime;

    public int initiative;

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

    public abstract IEnumerator Action(List<GameObject> enemies, List<GameObject> friends);

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
