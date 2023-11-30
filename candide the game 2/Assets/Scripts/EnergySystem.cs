using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public int maxEnergy;
    public int currentEnergy;

    public List<GameObject> energyDots;

    // Start is called before the first frame update
    void Start()
    {
        maxEnergy = 3;
        currentEnergy = 0;
        SetEnergyDots();
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

        SetEnergyDots();

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

        SetEnergyDots();

    }

    private void SetEnergyDots()
    {
        for (int i = 0; i < energyDots.Count; i++)
        {
            energyDots[i].gameObject.SetActive(false);
        }

        if(currentEnergy > 0)
        {
            for (int i = 0; i < currentEnergy; i++)
            {
                energyDots[i].SetActive(true);
            }
        }
    }
}
