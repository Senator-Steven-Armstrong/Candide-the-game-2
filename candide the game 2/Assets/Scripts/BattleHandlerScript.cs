using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleHandlerScript : MonoBehaviour
{
    public List<GameObject> playerEntites;
    public List<GameObject> enemyEntites;
    public List<BaseEntityScipt> attackQueue;
    public Vector3 friendAttackPos;
    public Vector3 enemyAttackPos;
    public GameObject ActionMenu;
    public GameObject BottomMenu;
    public BaseEntityScipt AttackingEntityScript;
    public bool icannotprocess;

    public bool hasSelectedInput;
    public float elapsedTime;

    public enum BattleStates
    {
        SWITCHINGENTITY,
        MOVINGTOBATTLE,
        WAITINGFORINPUT,
        CHOOSINGENTITY,
        EXECUTINGACTION,

    }

    public BattleStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        attackQueue = SortEntityByAttackInitiative();
        ActionMenu.SetActive(false);
        BottomMenu.SetActive(false);
        StartCoroutine(FullTurn());

        icannotprocess = false;
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < attackQueue.Count; i++)
        //{
        //    switch (currentState)
        //    {
        //        case BattleStates.SWITCHINGENTITY:
                    
        //            break;
        //        case BattleStates.WAITINGFORINPUT:
        //            if (hasSelectedInput)
        //            {
        //                currentState = BattleStates.CHOOSINGENTITY;
        //            }
        //            break;
        //        case BattleStates.CHOOSINGENTITY:
        //            break;
        //        case BattleStates.EXECUTINGACTION:
        //            break;
        //    }
        //}
    }

    private List<BaseEntityScipt> SortEntityByAttackInitiative()
    {
        List<BaseEntityScipt> list = new();
        foreach (GameObject ent in playerEntites)
        {
            list.Add(ent.GetComponent<BaseEntityScipt>());
        }
        foreach (GameObject ent in enemyEntites)
        {
            list.Add(ent.GetComponent<BaseEntityScipt>());
        }
        list = list.OrderBy(ent => ent.initiative).ToList();
        list.Reverse();
        return list;
    }

    public IEnumerator FullTurn()
    {
        for (int i = 0; i < attackQueue.Count; i++)
        {
            AttackingEntityScript = attackQueue[i];

            if (AttackingEntityScript.isPlayerControlled)
            {
                BottomMenu.SetActive(true);
            }
            yield return StartCoroutine(attackQueue[i].MoveToBattle());

            //Attackerar för dumma botar
            if (!attackQueue[i].isPlayerControlled)
            {
                int index = Random.Range(0, AttackingEntityScript.attackScripts.Count);
                AttackingEntityScript.attackScripts[index].SetAnimator(AttackingEntityScript.animator);
                AttackingEntityScript.attackScripts[index].Action(playerEntites, enemyEntites, AttackingEntityScript);  
            }

            //väntar på input från dumma spelaren
            if (attackQueue[i].isPlayerControlled)
            {
                yield return StartCoroutine(WaitForInput());
            }
            
            yield return StartCoroutine(attackQueue[i].MoveFromBattle());

            BottomMenu.SetActive(false);
            ActionMenu.SetActive(false);
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(FullTurn());
    }

    public IEnumerator WaitForInput()
    {
        bool isWaiting = true;

        while (isWaiting == true)
        {
            if(icannotprocess == true)
            {
                icannotprocess = false;
                isWaiting = false;
                
            }
            yield return null;
        }
        
    }


}
