using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    [SerializeField]private Vector2 numOfCombatRoomsRange;
    [SerializeField]private int _numOfCombatRooms;
    private int currentNumOfRooms;
    public int totalRoomMax;

    private enum Directions
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    private Directions _generalDirection;

    [Header("Room Prefabs")]
    public GameObject BasicRoom;
    public GameObject CombatRoom;

    // Start is called before the first frame update
    void Start()
    {
        _numOfCombatRooms = Mathf.RoundToInt(Random.Range(numOfCombatRoomsRange.x, numOfCombatRoomsRange.y));
        StartGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGeneration()
    {
        GameObject room = Instantiate(BasicRoom, Vector3.zero, Quaternion.identity);
        currentNumOfRooms++;

        _generalDirection = GetRandomEnumValue();
        GenerateNextRoom();
    }

    private void GenerateNextRoom()
    {
        
    }

    private Directions GetRandomEnumValue()
    {
        Directions direction = (Directions)Random.Range(0, 3);
        return direction;
    }

}
