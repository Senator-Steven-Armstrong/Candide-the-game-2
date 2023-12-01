using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    public List<GameObject> roomObjects;
    public List<Room> rooms;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            rooms.Add(gameObject.transform.GetChild(i).GetComponent<Room>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}