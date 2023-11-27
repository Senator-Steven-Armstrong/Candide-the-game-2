using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

            Attacks[i].SetVariables();

            GameObject AttackUI = Instantiate(ActionUIPrefab, ActionSpaces[i].transform);
            OccupiedActionSpaces.Add(AttackUI);
            AttackUI.transform.SetParent(ActionSpaces[i].transform);

            // Länkar ihop knappen och attacken mycket mycket viktig
            Attacks[i].buttonScript = AttackUI.GetComponent<ActionButtonScript>();
            Attacks[i].buttonScript.SetVariables(Attacks[i].stringName, Attacks[i].stringAttackDamage, Attacks[i].stringDebateDamage, Attacks[i].stringDescription);

            UnityAction action = () => Attacks[i2].Action(battleHandlerScript.enemyEntitiesAlive, battleHandlerScript.playerEntitiesAlive, battleHandlerScript.AttackingEntityScript);
            UnityAction stupid = () => SetStupidVariable();

            // fixar button press för attacken
            Button button = AttackUI.GetComponent<Button>();
            button.onClick.AddListener(action);
            button.onClick.AddListener(stupid);
            if (Attacks[i2].willChooseTargets)
            {

            }
            else
            {
                gameObject.SetActive(false);
                button.onClick.AddListener(stupid);
            }
        }
    }

    public void FillActionPanelDebate()
    {
        List<BaseDebateScript> Debates = battleHandlerScript.AttackingEntityScript.debateScripts;

        for (int i = 0; i < Debates.Count; i++)
        {
            int i2 = i;
            Debates[i].SetVariables();

            GameObject DebateUI = Instantiate(ActionUIPrefab, ActionSpaces[i].transform);
            OccupiedActionSpaces.Add(DebateUI);
            DebateUI.transform.SetParent(ActionSpaces[i].transform);

            // Länkar ihop knappen och attacken mycket mycket viktig
            Debates[i].buttonScript = DebateUI.GetComponent<ActionButtonScript>();
            Debates[i].buttonScript.SetVariables(Debates[i].stringName, Debates[i].stringAttackDamage, Debates[i].stringDebateDamage, Debates[i].stringDescription);

            // fixar button press för attacken'
            UnityAction action = () => Debates[i2].Action(battleHandlerScript.enemyEntitiesAlive, battleHandlerScript.playerEntitiesAlive, battleHandlerScript.AttackingEntityScript);
            UnityAction stupid = () => SetStupidVariable();

            Button button = DebateUI.GetComponent<Button>();
            button.onClick.AddListener(action);
            if (Debates[i2].willChooseTargets)
            {

            }
            else
            {
                battleHandlerScript.ActionMenu.SetActive(false);
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

    public void SetStupidVariable()
    {
        battleHandlerScript.icannotprocess = true;
    }

}
