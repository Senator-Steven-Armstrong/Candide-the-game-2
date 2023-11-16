using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonScript : MonoBehaviour
{
    public Text stringName;
    public Text attackDamage;
    public Text debateDamage;

    public void SetVariables(string name, string attackDmg, string debateDmg)
    {
        stringName.text = name;
        attackDamage.text = attackDmg;
        debateDamage.text = debateDmg;
    }


}
