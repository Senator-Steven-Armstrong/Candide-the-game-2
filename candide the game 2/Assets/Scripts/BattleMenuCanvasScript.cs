using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Collections.Specialized.BitVector32;

public class BattleMenuCanvasScript : MonoBehaviour
{
    public GameObject BottomMenu;
    public GameObject ActionMenu;
    public GameObject LastClickedButton;
    public GameObject AttackButton;
    public GameObject DebateButton;
    public BattleHandlerScript battleHandlerScript;

    public List<GameObject> ActionSpaces = new();
    public List<GameObject> OccupiedActionSpaces = new();
    public GameObject ActionUIPrefab;
    public GameObject EntityButtonPrefab;
    public GameObject selectArrowPrefab;
    public GameObject arrowHolder;

    public BaseActionScript currentAction;

    // Start is called before the first frame update
    void Start()
    {
        
        battleHandlerScript = GameObject.Find("BattleHandler").GetComponent<BattleHandlerScript>();
    }

    public void ShowActionMenu(GameObject buttonClicked)
    {

        ResetActionPanel();

        // checkar första gången click så det itne blir några buggar
        if(LastClickedButton == null)
        {
            LastClickedButton = buttonClicked;
        }
 
        if(LastClickedButton == buttonClicked)
        {
            if (ActionMenu.activeSelf == true)
            {
                ActionMenu.SetActive(false);
            }
            else
            {
                ActionMenu.SetActive(true);

                if (buttonClicked == AttackButton)
                {
                    Debug.Log("its killing time");
                    FillActionPanel(true);
                }
                else if (buttonClicked == DebateButton)
                {
                    Debug.Log("its debating time");
                    FillActionPanel(false);
                }
            }
        }
        else
        {
            ActionMenu.SetActive(false);
            ActionMenu.SetActive(true);

            if (buttonClicked == AttackButton)
            {
                Debug.Log("its killing time");
                FillActionPanel(true);
            }
            else if (buttonClicked == DebateButton)
            {
                Debug.Log("its debating time");
                FillActionPanel(false);
            }
        }

        LastClickedButton = buttonClicked;
    }

    public void CheckSelectedEntity(GameObject entity)
    {
        
        //När en entity är vald
        Debug.Log(entity);  
        if(currentAction.canOnlySelectDifferentTypes)
        {
            if(!currentAction.selectedEntites.Contains(entity))
            {
                currentAction.selectedEntites.Add(entity);
            }
        }
        else
        {
            currentAction.selectedEntites.Add(entity);
        }

        currentAction = battleHandlerScript.AttackingEntityScript.currentAction;

        // Om alla entities är valda
        if(currentAction.selectedEntites.Count >= currentAction.numOfEntitesToSelect)
        {
            // stoppa slection och kör action
            currentAction.Action(currentAction.selectedEntites, battleHandlerScript.AttackingEntityScript);

            //förstör pilarna
            for (int i = 0; i < currentAction.possibleEntitiesToSelect.Count; i++)
            {
                Destroy(arrowHolder.transform.GetChild(i).gameObject);
            }

            //stänger av hitbox
            for (int i = 0; i < currentAction.possibleEntitiesToSelect.Count; i++)
            {
                currentAction.possibleEntitiesToSelect[i].gameObject.GetComponent<Collider2D>().enabled = false;
            }

            CameraBehaviourScript cam = Camera.main.GetComponent<CameraBehaviourScript>();
            StartCoroutine(cam.MoveToBattlePos(0.2f));

            StopWaitingForInputVariable();
        }
    }

    public void MakeEntitiesSelectable(List<GameObject> selectableEntities)
    {
        CameraBehaviourScript cam = Camera.main.GetComponent<CameraBehaviourScript>();
        StartCoroutine(cam.MoveToOverviewPos(0.3f));
        BottomMenu.SetActive(false);
        ActionMenu.SetActive(false);

        for (int i = 0; i < selectableEntities.Count; i++)
        {
            BaseEntityScipt entityScript = selectableEntities[i].GetComponent<BaseEntityScipt>();
            Vector3 spawnPosition = entityScript.BarsSpriteHolder.transform.position;
            spawnPosition.x = selectableEntities[i].transform.position.x;
            spawnPosition.y += 0.5f;
            GameObject arrow = Instantiate(selectArrowPrefab, spawnPosition, Quaternion.identity);
            arrow.transform.SetParent(arrowHolder.transform, true);
            entityScript.selectArrow = arrow;

            selectableEntities[i].GetComponent<Collider2D>().enabled = true;
            
        }
    }

    public void FillActionPanel(bool isAttack)
    {
        List<BaseActionScript> actions = new();

        if (isAttack)
        {
            for (int i = 0; i < battleHandlerScript.AttackingEntityScript.attackScripts.Count; i++)
            {
                actions.Add(battleHandlerScript.AttackingEntityScript.attackScripts[i]);
            }
        }
        else
        {
            for (int i = 0; i < battleHandlerScript.AttackingEntityScript.debateScripts.Count; i++)
            {
                actions.Add(battleHandlerScript.AttackingEntityScript.debateScripts[i]);
            }
        }

        for (int i = 0; i < actions.Count; i++)
        {
            int i2 = i;

            actions[i2].SetVariables(battleHandlerScript.enemyEntitiesAlive, battleHandlerScript.playerEntitiesAlive);

            GameObject AttackUI = Instantiate(ActionUIPrefab, ActionSpaces[i].transform);
            Button button = AttackUI.GetComponent<Button>();
            ActionButtonScript buttonScript = button.gameObject.GetComponent<ActionButtonScript>();
            buttonScript.action = actions[i2];
            OccupiedActionSpaces.Add(AttackUI);
            AttackUI.transform.SetParent(ActionSpaces[i].transform);

            // Länkar ihop knappen och attacken mycket mycket viktig
            actions[i2].buttonScript = AttackUI.GetComponent<ActionButtonScript>();
            actions[i2].buttonScript.SetVariables(actions[i2].stringName, actions[i2].stringAttackDamage, actions[i2].stringDebateDamage, actions[i2].stringDescription);

            UnityAction checkAction = () => CheckEnergyOnClick(() => actions[i2].ChooseEntities(battleHandlerScript.enemyEntitiesAlive, battleHandlerScript.playerEntitiesAlive, battleHandlerScript.AttackingEntityScript), buttonScript);
            UnityAction checkWaitingForInput = () => CheckEnergyOnClick(() => StopWaitingForInputVariable(), buttonScript);
            UnityAction checkButtonDisable = () => CheckEnergyOnClick(() => DisableButton(button), buttonScript);

            UnityAction bruh = () => SetCurrentAction(buttonScript);

            // fixar button press för attacken
            button.onClick.AddListener(checkButtonDisable);
            button.onClick.AddListener(bruh);

            if (!actions[i2].willChooseTargets)
            {
                button.onClick.AddListener(checkAction);
                button.onClick.AddListener(checkWaitingForInput); 
            }
            else
            {
                UnityAction checkEntitiesSelectable = () => CheckEnergyOnClick(() => MakeEntitiesSelectable(buttonScript.action.possibleEntitiesToSelect), buttonScript);
                button.onClick.AddListener(checkEntitiesSelectable);
            }
        }
    }

    public void SetCurrentAction(ActionButtonScript script)
    {
        battleHandlerScript.AttackingEntityScript.currentAction = script.action;
        currentAction = script.action;
     
    }

    public void CheckEnergyOnClick(UnityAction function, ActionButtonScript script)
    {
        if (script.action.CheckEnergy(battleHandlerScript.AttackingEntityScript))
        {
            function.Invoke();
        }
    }

    public void ResetActionPanel()
    {
        if(OccupiedActionSpaces.Count > 0)
        {
            for (int i = 0; i < OccupiedActionSpaces.Count; i++)
            {
                if (OccupiedActionSpaces[i] != null)
                {
                    Destroy(OccupiedActionSpaces[i]);
                }
            }
        }  
    }

    public void StopWaitingForInputVariable()
    {
        battleHandlerScript.icannotprocess = true;
    }

    public void DisableButton(Button button)
    {
        button.enabled = false;
    }

}
