using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseActionScript
{
    public string stringName;
    public string stringAttackDamage;
    public string stringDebateDamage;
    public string stringDescription;
    public ActionButtonScript buttonScript;
    [NonSerialized]public bool icannotprocess = false;
    public bool willChooseTargets = false;

    public int numOfEntitesToSelect;
    public bool canOnlySelectDifferentTypes;
    public List<GameObject> selectedEntites = new List<GameObject>();
    public List<GameObject> possibleEntitiesToSelect = new List<GameObject>();

    public abstract void Action(List<GameObject> affectedEntites, BaseEntityScipt currentEntity);

    public abstract void SetVariables(List<GameObject> enemies = null, List<GameObject> friends = null);

    public void ExtendVariables()
    {
        stringAttackDamage = "ATK " + stringAttackDamage;
        stringDebateDamage = "DEB " + stringDebateDamage;
    }

    public IEnumerator WaitForInput()
    {
        bool isWaiting = true;

        while (isWaiting == true)
        {
            if (icannotprocess == true)
            {
                icannotprocess = false;
                isWaiting = false;

            }
            yield return null;
        }
    }

    public abstract void ChooseEntities(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity);

    public void SetupEntityButtonList(List<GameObject> entitesToShow)
    {
        BattleMenuCanvasScript battleMenuCanvasScript = GameObject.Find("Battle Menu Canvas").GetComponent<BattleMenuCanvasScript>();

        
    }

    public void EntityButtonClick()
    {

    }
        
}
