using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActionScript : MonoBehaviour
{
    public string attackName;
    public string stringDamage;

    public abstract void Action(List<GameObject> enemies, List<GameObject> friends);

    public abstract void SpecialAction();
}
