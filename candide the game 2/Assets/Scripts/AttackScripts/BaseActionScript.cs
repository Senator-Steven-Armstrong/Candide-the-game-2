using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActionScript
{
    public string stringName;
    public string stringAttackDamage;
    public string stringDebateDamage;
    public ActionButtonScript buttonScript;
    public Animator animator;

    public abstract void Action(List<GameObject> enemies, List<GameObject> friends, BaseEntityScipt currentEntity);

    public abstract void SetVariables();

    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void ExtendVariables()
    {
        stringAttackDamage = "ATK " + stringAttackDamage;
        stringDebateDamage = "DEB " + stringDebateDamage;
    }
}
