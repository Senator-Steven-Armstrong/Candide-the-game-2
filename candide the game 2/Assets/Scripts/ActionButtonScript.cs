using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButtonScript : MonoBehaviour
{
    public Text stringName;
    public Text attackDamage;
    public Text debateDamage;
    public Text description;
    public BaseActionScript action;
    public List<GameObject> costDots;
    public List<GameObject> generationDots;

    public void SetVariables(string name, string attackDmg, string debateDmg, string desc, int cost, int generated)
    {
        stringName.text = name;
        attackDamage.text = attackDmg;
        debateDamage.text = debateDmg;
        description.text = desc;
        setEnergyDots(cost, costDots);
        setEnergyDots(generated, generationDots);
    }

    private void setEnergyDots(int num, List<GameObject> objectList)
    {
        if(num > 0)
        {
            for (int i = 0; i < num; i++)
            {
                objectList[i].SetActive(true);
            }
        }
    }

}
