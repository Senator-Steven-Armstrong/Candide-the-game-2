using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleHandlerScript : MonoBehaviour
{
    public List<GameObject> playerEntities;
    public List<GameObject> enemyEntities;
    public List<GameObject> playerEntitiesAlive;
    public List<GameObject> enemyEntitiesAlive;
    public List<BaseEntityScipt> attackQueue;
    public GameObject ActionMenu;
    public GameObject BottomMenu;
    public BaseEntityScipt AttackingEntityScript;
    public bool icannotprocess;

    [SerializeField] private List<Vector3> playerStartPositions;
    [SerializeField] private List<Vector3> enemyStartPositions;

    [SerializeField] private Vector3 playerBattlePos = new Vector3(-1, 0, 0);
    [SerializeField] private Vector3 enemyBattlePos = new Vector3(1, 0, 0);

    public bool hasSelectedInput;
    public float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEntities();


        icannotprocess = false;
        attackQueue = SortEntityByAttackInitiative();
        ActionMenu.SetActive(false);
        BottomMenu.SetActive(false);
        StartCoroutine(FullTurn());
        
    }

    private void SpawnEntities()
    {
        //spawn players
        for (int i = 0; i < playerEntities.Count; i++)
        {
            GameObject playerEntity = Instantiate(playerEntities[i]);
            playerEntitiesAlive.Add(playerEntity);
            playerEntity.transform.position = playerStartPositions[i];
            BaseEntityScipt script = playerEntity.GetComponent<BaseEntityScipt>();
            script.startPosition = playerStartPositions[i];
            script.battlePosition = playerBattlePos;
        }

        //spawn enemies
        for (int i = 0; i < enemyEntities.Count; i++)
        {
            GameObject enemyEntity = Instantiate(enemyEntities[i]);
            enemyEntitiesAlive.Add(enemyEntity);
            enemyEntity.transform.position = enemyStartPositions[i];
            BaseEntityScipt script = enemyEntity.GetComponent<BaseEntityScipt>();
            script.startPosition = enemyStartPositions[i];
            script.battlePosition = enemyBattlePos;
        }
    }

    private List<BaseEntityScipt> SortEntityByAttackInitiative()
    {
        List<BaseEntityScipt> list = new();
        foreach (GameObject ent in playerEntitiesAlive)
        {
            list.Add(ent.GetComponent<BaseEntityScipt>());
        }
        foreach (GameObject ent in enemyEntitiesAlive)
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

           
            yield return new WaitForSeconds(1f);

            AttackingEntityScript = attackQueue[i];

            if (AttackingEntityScript.isPlayerControlled)
            {
                BottomMenu.SetActive(true);
            }
            yield return StartCoroutine(attackQueue[i].MoveToBattle());

            //Attackerar för dumma botar
            if (!attackQueue[i].isPlayerControlled)
            {
                attackQueue[i].AiChooseMove(playerEntitiesAlive, enemyEntitiesAlive, AttackingEntityScript);  
            }

            //väntar på input från dumma spelaren
            else if (attackQueue[i].isPlayerControlled)
            {
                yield return StartCoroutine(WaitForInput());

            }
            
            yield return StartCoroutine(attackQueue[i].MoveFromBattle());

            BottomMenu.SetActive(false);
            ActionMenu.SetActive(false);

            CheckAliveEntities();

        }

        if(playerEntitiesAlive.Count == 0 || enemyEntitiesAlive.Count == 0)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(1);
        }
        else
        {
            StartCoroutine(FullTurn());
        }

        
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

    public void CheckAliveEntities()
    {
        for (int i = 0; i < attackQueue.Count; i++)
        {
            if (attackQueue[i].healthSystem.currentHealth <= 0 || attackQueue[i].moralitySystem.currentMorality <= 0)
            {
                if (attackQueue[i].isPlayerControlled == true)
                {
                    playerEntitiesAlive.Remove(attackQueue[i].gameObject);
                }
                else
                {
                    enemyEntitiesAlive.Remove(attackQueue[i].gameObject);
                }
                attackQueue.Remove(attackQueue[i]);
                i--;
            }
        }
    }
}
