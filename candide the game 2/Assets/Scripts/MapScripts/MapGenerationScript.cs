using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    [Header("Generation settings")]
    [SerializeField] private Vector2 numOfBasicRoomsRange;
    [SerializeField] private int _numOfPossibleBasicRooms;
    private int _numOfCurrentBasicRooms;
    [SerializeField]private Vector2 numOfCombatRoomsRange;
    [SerializeField]private int _numOfPossibleCombatRooms;
    private int _numOfCurrentCombatRooms;

    //Direction is a value from 1-4, 1 being north and 4 being west, its a clockwise compass
    private int _direction;
    private List<int> _possibleDirections = new List<int>();
    [SerializeField] private int roomsUntilTurn;
    [SerializeField] private int roomsUntilCombat;

    private GameObject currentRoom;
    public PartyHandlerScript partyHandler;

    [Header("Room Prefabs")]
    public GameObject BasicRoom;
    public GameObject CombatRoom;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            _possibleDirections.Add(i+1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(gameObject.transform.childCount != 0)
            {
                int TransformChildCount = gameObject.transform.childCount;
                for (int i = 0; i < TransformChildCount; i++)
                {
                    Destroy(gameObject.transform.GetChild(TransformChildCount - i - 1).gameObject);
                }
            }

            SetGenerationValues();
            StartGeneration();
        }
    }

    void StartGeneration()
    {
        SpawnRoom(BasicRoom, Vector3.zero);
        partyHandler.currentRoom = currentRoom.GetComponent<Room>();

        Debug.Log(_numOfCurrentBasicRooms);
        Debug.Log(_numOfPossibleBasicRooms);
        if (_numOfCurrentBasicRooms < _numOfPossibleBasicRooms)
        {
            GenerateNextRoom();
        }
    }

    void SetGenerationValues()
    {
        _numOfCurrentBasicRooms = 0;
        _numOfCurrentCombatRooms = 0;

        SetRandomDirection();

        _numOfPossibleBasicRooms = Mathf.RoundToInt(Random.Range(numOfBasicRoomsRange.x, numOfBasicRoomsRange.y));
        _numOfPossibleCombatRooms = Mathf.RoundToInt(Random.Range(numOfCombatRoomsRange.x, numOfCombatRoomsRange.y));
        if(numOfBasicRoomsRange.x < 1)
        {
            _numOfPossibleBasicRooms = 1;
        }

        roomsUntilTurn = Random.Range(2, 2);

    }

    private void SetRandomDirection()
    {
        _direction = _possibleDirections[Random.Range(0, _possibleDirections.Count)];
    }

    private void GenerateNextRoom()
    {
        Vector3 nextRoomPosition = currentRoom.transform.position;
        // Sets the position of the next tile
        if (_direction % 2 == 0)
        {
            nextRoomPosition.x += GetDirectionPosValue(_direction);
        }
        else
        {
            nextRoomPosition.z += GetDirectionPosValue(_direction);
        }

        // Instantiates Room
        SpawnRoom(BasicRoom, nextRoomPosition);

        if(_numOfCurrentBasicRooms < _numOfPossibleBasicRooms)
        {
            GenerateNextRoom();
        }
    }

    private void SpawnRoom(GameObject prefab, Vector3 pos)
    {
        GameObject room = Instantiate(prefab, pos, Quaternion.identity);
        if (currentRoom != null)
        {
            Room previousRoomScript = currentRoom.GetComponent<Room>();

            Room currentRoomScript = room.GetComponent<Room>();
            switch (_direction)
            {
                case 1:
                    previousRoomScript.topAdjacentRoom = currentRoomScript;
                    currentRoomScript.bottomAdjacentRoom = previousRoomScript;
                    break;
                case 2:
                    previousRoomScript.rightAdjacentRoom = currentRoomScript;
                    currentRoomScript.leftAdjacentRoom = previousRoomScript;
                    break;
                case 3:
                    previousRoomScript.bottomAdjacentRoom = currentRoomScript;
                    currentRoomScript.topAdjacentRoom = previousRoomScript;
                    break;
                case 4:
                    previousRoomScript.leftAdjacentRoom = currentRoomScript;
                    currentRoomScript.rightAdjacentRoom = previousRoomScript;
                    break;
                default:
                    break;
            }
        }

        currentRoom = room;
        room.transform.SetParent(gameObject.transform, true);
        _numOfCurrentBasicRooms++;
    }

    int GetDirectionPosValue(int direction)
    {
        if(direction > 2)
        {
            return -10;
        }
        else
        {
            return 10;
        }
    }

}
