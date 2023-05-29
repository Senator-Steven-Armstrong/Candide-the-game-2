using System.Collections;
using System.Collections.Generic;
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

    protected Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentState = TurnState.SELECTING;
        startPosition = transform.position;
    }

    
}
