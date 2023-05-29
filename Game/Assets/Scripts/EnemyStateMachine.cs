using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : EntityStateMachine
{
    private BattleStateMachine BSM;
    public BaseEnemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        Debug.Log(BSM.playersInBattle);
        ChooseAction();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case TurnState.WAITING:

                break;
            case TurnState.SELECTING:

                break;
            case TurnState.ACTION:

                break;
            case TurnState.DEAD:

                break;
        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.attackerName = enemy.name;
        myAttack.attackerGameobject = this.gameObject;
        myAttack.TargetGameObject = BSM.playersInBattle[Random.Range(0, BSM.playersInBattle.Count)];
        BSM.CollectActions(myAttack);
    }
}
