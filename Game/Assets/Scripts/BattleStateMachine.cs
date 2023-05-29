using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                break;
            case (BattleStates.TAKEACTION):

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
