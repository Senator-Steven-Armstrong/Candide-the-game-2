using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : EntityStateMachine
{
    
    public BaseEnemy enemy;

    

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case TurnState.WAITING:
                break;
            case TurnState.SELECTING:
                ChooseAction();
                currentState = TurnState.WAITING;
                break;
            case TurnState.ACTION:
                StartCoroutine(TimeForAction());
                break;
            case TurnState.DEAD:

                break;
        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.attackerName = enemy.name;
        myAttack.entityType = "enemy";
        myAttack.attackerGameobject = this.gameObject;
        myAttack.TargetGameObject = BSM.playersInBattle[Random.Range(0, BSM.playersInBattle.Count)];
        BSM.CollectActions(myAttack);

    }
}
