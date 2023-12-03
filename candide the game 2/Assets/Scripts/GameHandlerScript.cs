using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandlerScript : MonoBehaviour
{
    private static GameHandlerScript instance;

    public List<GameObject> PartyMembers = new List<GameObject>();
    public List<GameObject> enemiesInCombat = new List<GameObject>();
    public GameObject currentMap;
    public GameObject baseCombatEnviornment;
    public GameObject currentCombatEnviornment;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
