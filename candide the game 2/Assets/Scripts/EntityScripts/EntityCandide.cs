using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EntityCandide : BaseEntityScipt
{
    public override void AiChooseMove(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity)
    {
        Debug.Log("hey its me candid");
    }

    private void Start()
    {
        SetVariables();

        attackScripts.Add(new AttackBasic());
        attackScripts.Add(new AttackBasic());
        debateScripts.Add(new DebateBasic());
    }

    private void Update()
    {
        MovePositionsChecker();
    }



}
