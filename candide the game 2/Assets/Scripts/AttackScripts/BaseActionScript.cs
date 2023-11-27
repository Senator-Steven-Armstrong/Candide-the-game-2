using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActionScript
{
    public string stringName;
    public string stringAttackDamage;
    public string stringDebateDamage;
    public string stringDescription;
    public ActionButtonScript buttonScript;
    [NonSerialized]public bool icannotprocess = false;
    public bool willChooseTargets = false;

    public abstract void Action(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity);

    public abstract void SetVariables();

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
}
