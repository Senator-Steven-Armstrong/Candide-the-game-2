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
                    FillActionPanelAttack();
                }
                else if (buttonClicked == DebateButton)
                {
                    Debug.Log("its debating time");
                    FillActionPanelDebate();
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
                FillActionPanelAttack();
            }
            else if (buttonClicked == DebateButton)
            {
                Debug.Log("its debating time");
                FillActionPanelDebate();
            }
        }

        LastClickedButton = buttonClicked;
    }

    public void FillActionPanelAttack()
    {
        List<BaseAttackScript> Attacks = battleHandlerScript.AttackingEntityScript.attackScripts;

        for (int i = 0; i < Attacks.Count; i++)
        {
            int i2 = i;

            Attacks[i].SetVariables(battleHandlerScript.enemyEntitiesAlive, battleHandlerScript.playerEntitiesAlive);

            GameObject AttackUI = Instantiate(ActionUIPrefab, ActionSpaces[i].transform);
            Button button = AttackUI.GetComponent<Button>();
            OccupiedActionSpaces.Add(AttackUI);
            AttackUI.transform.SetParent(ActionSpaces[i].transform);

            // Länkar ihop knappen och attacken mycket mycket viktig
            Attacks[i].buttonScript = AttackUI.GetComponent<ActionButtonScript>();
            Attacks[i].buttonScript.SetVariables(Attacks[i].stringName, Attacks[i].stringAttackDamage, Attacks[i].stringDebateDamage, Attacks[i].stringDescription);


            UnityAction action = () => Attacks[i2].ChooseEntities(battleHandlerScript.enemyEntitiesAlive, battleHandlerScript.playerEntitiesAlive, battleHandlerScript.AttackingEntityScript);
            UnityAction stopWaitingForInput = () => StopWaitingForInputVariable();
            UnityAction buttonDisable = () => DisableButton(button);

            // fixar button press för attacken
            button.onClick.AddListener(buttonDisable);

            if (!Attacks[i2].willChooseTargets)
            {
                button.onClick.AddListener(stopWaitingForInput);
                button.onClick.AddListener(action);
            }
            else
            {
                
                UnityAction fillPanelWithEntities = () => MakeEntitiesSelectable(Attacks[i2].possibleEntitiesToSelect, Attacks[i2]);
                Debug.Log(Attacks[i2].possibleEntitiesToSelect);
                button.onClick.AddListener(fillPanelWithEntities);
                
            }
        }
    }

    public void dosometing(GameObject entity)
    {
        Debug.Log(entity);  
    }

    public void FillActionPanelWithEntities(List<GameObject> targets)
    {
        ResetActionPanel();
        Debug.Log(targets);
        for (int i = 0; i < targets.Count; i++)
        {
            BaseEntityScipt entityScript = targets[i].GetComponent<BaseEntityScipt>();

            GameObject EntityUI = Instantiate(EntityButtonPrefab, ActionSpaces[i].transform);
            OccupiedActionSpaces.Add(EntityUI);
            EntityUI.transform.SetParent(ActionSpaces[i].transform);

            Button button = EntityUI.GetComponent<Button>();
            EntityButtonScript buttonScript = EntityUI.GetComponent<EntityButtonScript>();

            buttonScript.entity = targets[i];
            Debug.Log(buttonScript.entity);

            buttonScript.SetVariables();




            //med button variablen ska du lägga till onclick och typ returnera det objectet, lägga det i en lista och sedan när listan är tillcäkligt lång ska du binka och boonka
        }
    }

    public void MakeEntitiesSelectable(List<GameObject> selectableEntities, BaseActionScript action)
    {
        for (int i = 0; i < selectableEntities.Count; i++)
        {
            Vector3 spawnPosition = selectableEntities[i].GetComponent<BaseEntityScipt>().BarsSpriteHolder.transform.position;
            spawnPosition.x = selectableEntities[i].transform.position.x;
            spawnPosition.y += 0.5f;
            Instantiate(selectArrowPrefab, spawnPosition, Quaternion.identity);

            Collider2D collider = selectableEntities[i].GetComponent<Collider2D>();
            collider.enabled = true;

            
        }
    }

    //public void FillActionPanel(bool isAttack)
    //{
    //     actions = new();

    //    if (isAttack)
    //    {
    //        actions = battleHandlerScript.AttackingEntityScript.attackScripts;
    //    }
    //    else
    //    {
    //        List<BaseAttackScript> actions = new List<BaseAttackScript>();
    //    }

    //    for (int i = 0; i < actions.Count; i++)
    //    {
    //        int i2 = i;

    //        actions[i].SetVariables();

    //        GameObject AttackUI = Instantiate(ActionUIPrefab, ActionSpaces[i].transform);
    //        OccupiedActionSpaces.Add(AttackUI);
    //        AttackUI.transform.SetParent(ActionSpaces[i].transform);

    //        // Länkar ihop knappen och attacken mycket mycket viktig
    //        actions[i].buttonScript = AttackUI.GetComponent<ActionButtonScript>();
    //        actions[i].buttonScript.SetVariables(actions[i].stringName, actions[i].stringAttackDamage, actions[i].stringDebateDamage, actions[i].stringDescription);

    //        UnityAction action = () => actions[i2].Action(battleHandlerScript.enemyEntitiesAlive, battleHandlerScript.playerEntitiesAlive, battleHandlerScript.AttackingEntityScript);
    //        UnityAction stupid = () => SetStupidVariable();

    //        // fixar button press för attacken
    //        Button button = AttackUI.GetComponent<Button>();
    //        button.onClick.AddListener(action);
    //        if (!actions[i2].willChooseTargets)
    //        {
    //            button.onClick.AddListener(stupid);
    //        }
    //    }
    //}

    public void FillActionPanelDebate()
    {
        List<BaseDebateScript> Debates = battleHandlerScript.AttackingEntityScript.debateScripts;

        for (int i = 0; i < Debates.Count; i++)
        {
            int i2 = i;
            Debates[i].SetVariables();

            GameObject DebateUI = Instantiate(ActionUIPrefab, ActionSpaces[i].transform);
            Button button = DebateUI.GetComponent<Button>();
            OccupiedActionSpaces.Add(DebateUI);
            DebateUI.transform.SetParent(ActionSpaces[i].transform);

            // Länkar ihop knappen och attacken mycket mycket viktig
            Debates[i].buttonScript = DebateUI.GetComponent<ActionButtonScript>();
            Debates[i].buttonScript.SetVariables(Debates[i].stringName, Debates[i].stringAttackDamage, Debates[i].stringDebateDamage, Debates[i].stringDescription);

            // fixar button press för attacken'
            UnityAction action = () => Debates[i2].ChooseEntities(battleHandlerScript.enemyEntitiesAlive, battleHandlerScript.playerEntitiesAlive, battleHandlerScript.AttackingEntityScript);
            UnityAction stupid = () => StopWaitingForInputVariable();
            UnityAction buttonDisable = () => DisableButton(button);

            button.onClick.AddListener(action);
            button.onClick.AddListener(buttonDisable);

            if (!Debates[i2].willChooseTargets)
            {
                button.onClick.AddListener(stupid);
            }
            
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

    public void DisableAttackMenu()
    {
        
    }

    public void DisableButton(Button button)
    {
        button.enabled = false;
    }

}
