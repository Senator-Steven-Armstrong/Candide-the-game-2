using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    public List<Room> rooms;
    public Room startRoom;

    public GameObject baseBattleScenePrefab;
    public GameObject baseBattleScene;
    public GameObject currentBattleScene;

    // Start is called before the first frame update
    void Start()
    {
        baseBattleScene = baseBattleScenePrefab;
        currentBattleScene = baseBattleScene;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            rooms.Add(gameObject.transform.GetChild(i).GetComponent<Room>());
        }

        startRoom = rooms[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
