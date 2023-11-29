using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public int maxEnergy;
    public int currentEnergy;

    // Start is called before the first frame update
    void Start()
    {
        maxEnergy = 3;
        currentEnergy = 0;   
    }

    public void DecreaseEnergy(int amount)
    {
        if(currentEnergy - amount < 0)
        {
            currentEnergy = 0;
        }
        else
        {
            currentEnergy -= amount;
        }
    }

    public void IncreaseEnergy(int amount)
    {
        if(currentEnergy + amount > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            currentEnergy += amount;
        }
    }
}
