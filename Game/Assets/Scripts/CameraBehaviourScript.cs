using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourScript : MonoBehaviour
{
    private float elapsedTime;
    public float moveTime;

    public Vector3 OverviewPosition = Vector3.zero;
    public Vector3 BattlePostion;

    public enum MoveStates
    {
        WAIT,
        MOVETOBATTLEVIEW,
        MOVETOOVERVIEW
    }

    public MoveStates currentState;


    // Start is called before the first frame update
    void Start()
    {
        currentState = MoveStates.WAIT;
        if(OverviewPosition != Vector3.zero)
        {
            gameObject.transform.position = OverviewPosition; 
        }
        else
        {
            OverviewPosition = gameObject.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (MoveStates.WAIT):
                elapsedTime = 0;
                break;
            case (MoveStates.MOVETOOVERVIEW):
                MoveBackToStartPos();
                break;
            case (MoveStates.MOVETOBATTLEVIEW):
                MoveIntoBattlePos();
                break;
        }
    }

    private void MoveIntoBattlePos()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / moveTime;
        gameObject.transform.position = Vector3.Lerp(OverviewPosition, BattlePostion, percentageComplete);
    }

    private void MoveBackToStartPos()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / moveTime;
        gameObject.transform.position = Vector3.Lerp(BattlePostion, OverviewPosition, percentageComplete);
    }

    public IEnumerator MoveToBattlePos(float entityMoveTime)
    {
        moveTime = entityMoveTime;
        currentState = MoveStates.MOVETOBATTLEVIEW;
        yield return new WaitForSeconds(moveTime);
        currentState = MoveStates.WAIT;
    }
    public IEnumerator MoveToOverviewPos(float entityMoveTime)
    {
        moveTime = entityMoveTime;
        currentState = MoveStates.MOVETOOVERVIEW;
        yield return new WaitForSeconds(moveTime);
        currentState = MoveStates.WAIT;
    }
}
