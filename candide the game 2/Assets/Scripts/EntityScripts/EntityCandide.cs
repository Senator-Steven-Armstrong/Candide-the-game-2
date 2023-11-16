using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EntityCandide : BaseEntityScipt
{   
    private void Start()
    {
        cameraBehaviourScript = Camera.main.GetComponent<CameraBehaviourScript>();
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
