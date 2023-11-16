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

    // Start is called before the first frame update
    void Start()
    {
        attackQueue = SortEntityAttackInitiative();
        ActionMenu.SetActive(false);
        BottomMenu.SetActive(false);
        StartCoroutine(FullTurn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<BaseEntityScipt> SortEntityAttackInitiative()
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
            if(AttackingEntityScript.isPlayerControlled)
            {
                BottomMenu.SetActive(true);
            }
            yield return StartCoroutine(attackQueue[i].Action(enemyEntites, playerEntites));
            BottomMenu.SetActive(false);
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(FullTurn());
    }

    public void SortAttacksInUI()
    {
        for(int i = 0;i < AttackingEntityScript.attackScripts.Count;i++)
        {

        }
    }
}
