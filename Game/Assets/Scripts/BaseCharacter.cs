using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCharacter
{
    public string name;

    public float maxHP;
    public float curHP;

    //MR stands for morality
    public float maxMR;
    public float curMR;

    public int maxEnergy;
    public int curEnergy;

    public int strength;
    public int defence; 
    public int metaPhysic;
    public int mentalFortitude;

}
