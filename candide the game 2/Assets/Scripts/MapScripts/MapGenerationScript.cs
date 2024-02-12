using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    [Header("Generation settings")]
    [SerializeField] private Vector2 numOfBasicRoomsRange;
    [SerializeField] public int _numOfPossibleBasicRooms;
    private int _numOfCurrentBasicRooms;
    [SerializeField]private Vector2 numOfCombatRoomsRange;
    [SerializeField]public int _numOfPossibleCombatRooms;
    private int _numOfCurrentCombatRooms;
    [SerializeField] private Vector2 numOfBranchesRange;
    [SerializeField] public int _numOfPossibleBranches;
    private int _numOfCurrentBranches;

    public bool isMainRoute = true;

    //Direction is a value from 1-4, 1 being north and 4 being west, its a clockwise compass
    private int _direction;
    private List<int> _possibleDirections = new List<int>();
    [SerializeField] private int roomsUntilTurn;
    [SerializeField] private int roomsUntilCombat;
    [SerializeField] private int roomsUntilBranch;

    public GameObject currentRoom;
    public PartyHandlerScript partyHandler;

    [Header("Room Prefabs")]
    public GameObject BasicRoom;
    public GameObject CombatRoom;

    public GameObject GeneratorPrefab;

    [Header("Miscellanious")]
    public Vector3 firstRoomSpawn = Vector3.zero;

    [SerializeField] public float waittime;

    public bool manualGeneration = false;
    public float savedWattime;

    private enum generationEvents
    {
        NORMAL,
        COMBAT,
        SHOP,
        SPLIT,
        LOOP,
        BOSS
    }

    [SerializeField] private generationEvents _currentGenerationEvent;

    // Start is called before the first frame update
    void Start()
    {
        savedWattime = waittime;

        if (!isMainRoute)
        {
            SetGenerationValues();
            StartGeneration();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            waittime = savedWattime;
            if(gameObject.transform.childCount != 0)
            {
                int TransformChildCount = gameObject.transform.childCount;
                for (int i = 0; i < TransformChildCount; i++)
                {
                    Destroy(gameObject.transform.GetChild(TransformChildCount - i - 1).gameObject);
                }
            }
            if (!isMainRoute)
            {
                Destroy(gameObject);
            }

            SetGenerationValues();
            StartGeneration();
        }

        if (Input.GetKeyDown(KeyCode.W) && manualGeneration == true)
        {
            waittime = 0;
            StartCoroutine(GenerateNextRoom());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            manualGeneration = !manualGeneration;
            if (manualGeneration)
            {
                waittime = 0;
            }
            else
            {
                waittime = savedWattime;
            }
        }
    }

    void StartGeneration()
    {
        SpawnRoom(BasicRoom, firstRoomSpawn);
        if(isMainRoute)
        {
            //Sätter startrummet som currentroom så man spawnar på rätt rum
            partyHandler.currentRoom = currentRoom.GetComponent<Room>();
        }

        if (_numOfCurrentBasicRooms < _numOfPossibleBasicRooms && !manualGeneration)
        {
            StartCoroutine(GenerateNextRoom());
        }
    }

    void SetGenerationValues()
    {
        _numOfCurrentBasicRooms = 0;
        _numOfCurrentCombatRooms = 0;

        

        if (isMainRoute)
        {
            ResetDirections();
            _numOfPossibleBasicRooms = Mathf.RoundToInt(Random.Range(numOfBasicRoomsRange.x, numOfBasicRoomsRange.y));
            _numOfPossibleCombatRooms = Mathf.RoundToInt(Random.Range(numOfCombatRoomsRange.x, numOfCombatRoomsRange.y));
            _numOfPossibleBranches = Mathf.RoundToInt(Random.Range(numOfBranchesRange.x, numOfBranchesRange.y));
        }

        _direction = Random.Range(1, 4);


        if(numOfBasicRoomsRange.x < 1)
        {
            _numOfPossibleBasicRooms = 1;
        }

        roomsUntilTurn = Random.Range(2, 4);
        roomsUntilBranch = (int)_numOfPossibleBasicRooms/3;

    }

    private void SetEventList()
    {
        int eventCount = (int)_numOfCurrentBasicRooms / 2;

    }

    private void ResetDirections()
    {
        if(_possibleDirections.Count > 0)
        {
            _possibleDirections.Clear();
        }
        
        for (int i = 0; i < 4; i++)
        {
            _possibleDirections.Add(i + 1);
        }
        _possibleDirections.Sort();
    }

    private int ChangeDirection()
    {
        RaycastHit hit;

        ResetDirections();
        _possibleDirections.Sort();
        _possibleDirections.Remove(_direction);
        _possibleDirections.Remove(GetReversedDirection());
        
        
        int chosenDirection = _possibleDirections[Random.Range(0, _possibleDirections.Count)];
        _possibleDirections.Remove(chosenDirection); 

        Vector3 rayDirection;
        rayDirection = GetPositionOfNextRoom(chosenDirection, Vector3.zero);


        if (Physics.Raycast(currentRoom.transform.position, rayDirection.normalized, out hit, 10000))
        {
            Debug.Log(hit.distance + " : Hit " + hit.collider.gameObject.name);

            //eftersom vi bara har 4 möjliga riktningar och 3 är borttagna återstår bara en
            chosenDirection = _possibleDirections[0];
            rayDirection = GetPositionOfNextRoom(chosenDirection, Vector3.zero);

            
            if (Physics.Raycast(currentRoom.transform.position, rayDirection.normalized, out hit, 10000))
            {
                Debug.Log("Did it again");
                Debug.Log(hit.distance + " : Hit " + hit.collider.gameObject.name);
                roomsUntilTurn = 1;
                Debug.Log("DIRECTION: " + _direction);
                return _direction;
            }
            else
            {
                roomsUntilTurn = Random.Range(1, 3);
                return chosenDirection;
            }
        }
        else
        {
            Debug.Log("Hit nothing");
            roomsUntilTurn = Random.Range(1, 3);
            return chosenDirection;
        }
        
    }

    private int GetReversedDirection()
    {
        switch (_direction)
        {
            case 1:
                return 3;
            case 2:
                return 4;
            case 3:
                return 1;
            case 4:
                return 2;
        }
        return 1;
    }

    private IEnumerator  GenerateNextRoom()
    {
        yield return new WaitForSeconds(waittime/1.5f);

        if (roomsUntilTurn <= 0)
        {
            _direction = ChangeDirection();
        }
        yield return new WaitForSeconds(waittime/1.5f);

        Vector3 nextRoomPosition = currentRoom.transform.position;
        // Sets the position of the next tile
        nextRoomPosition = GetPositionOfNextRoom(_direction, nextRoomPosition);

        // Instantiates Room
        SpawnRoom(BasicRoom, nextRoomPosition);
        roomsUntilTurn--;
        roomsUntilBranch--;

        if(roomsUntilBranch <= 0 && isMainRoute && _numOfCurrentBranches < _numOfPossibleBranches)
        {
            GenerateBranch();

        }

        if (_numOfCurrentBasicRooms < _numOfPossibleBasicRooms)
        {
            if(!manualGeneration) 
            StartCoroutine(GenerateNextRoom());
        }
    }

    private void GenerateBranch()
    {
        Debug.Log("generating branch at: x: " + currentRoom.transform.position.x + " | x: " + currentRoom.transform.position.y + " | y: " + currentRoom.transform.position.z + " | z: ");
        Vector3 generatorSpawnPoint = GetPositionOfNextRoom(ChangeDirection(), currentRoom.transform.position);
        GameObject generator = Instantiate(GeneratorPrefab, Vector3.zero, Quaternion.identity);
        MapGenerationScript generationScript = generator.GetComponent<MapGenerationScript>();
        generationScript.currentRoom = generator;
        generationScript.isMainRoute = false;
        generationScript.firstRoomSpawn = generatorSpawnPoint;
        generationScript._numOfPossibleBasicRooms = Random.Range((int)numOfBasicRoomsRange.x / 2, (int)numOfBasicRoomsRange.y / 2);
        generationScript.roomsUntilTurn = 10;
        generationScript.manualGeneration = manualGeneration;

        if (currentRoom != null)
        {
            Room mainRoomScript = currentRoom.GetComponent<Room>();

            Room branchRooScript = generationScript.currentRoom.GetComponent<Room>();
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

        roomsUntilBranch = (int)_numOfPossibleBasicRooms / 3;
        _numOfCurrentBranches++;
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

    private Vector3 GetPositionOfNextRoom(int direction, Vector3 position) 
    {
        if (direction % 2 == 0)
        {
            position.x += GetDirectionPosValue(direction);
        }
        else
        {
            position.z += GetDirectionPosValue(direction);
        }
        return position;
    }

}
