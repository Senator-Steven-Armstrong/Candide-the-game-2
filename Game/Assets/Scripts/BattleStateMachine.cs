using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntityStateMachine;

public class BattleStateMachine : MonoBehaviour
{

    public enum BattleStates
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    public BattleStates currentBattleState;

    public List<HandleTurn> actionList = new List<HandleTurn>();
    public List<GameObject> playersInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    { 
        currentBattleState = BattleStates.WAIT;  
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        playersInBattle.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBattleState)
        {
            case (BattleStates.WAIT):
                if (actionList.Count > 0)
                {
                    currentBattleState = BattleStates.TAKEACTION;
                }
                break;
            case (BattleStates.TAKEACTION):
                GameObject attacker = GameObject.Find(actionList[0].attackerName);
                if (actionList[0].entityType == "enemy")
                {
                    EnemyStateMachine ESM = attacker.GetComponent<EnemyStateMachine>();
                    ESM.attackPosition = new Vector3(1.5f, 1.6f, 1);
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }
                
                if (actionList[0].entityType == "player")
                {

                }
                currentBattleState = BattleStates.PERFORMACTION;
                break;
            case (BattleStates.PERFORMACTION):

                break;
        }
    }

    public void CollectActions(HandleTurn action)
    {
        actionList.Add(action);
    }
}
