using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStrMapGenScript : MonoBehaviour
{
    [Header("Generation settings")]
    [SerializeField] private Vector2 numOfBasicRoomsRange;
    [SerializeField] public int _numOfPossibleBasicRooms;
    private int _numOfCurrentBasicRooms;
    [SerializeField] private Vector2 numOfCombatRoomsRange;
    [SerializeField] public int _numOfPossibleCombatRooms;
    private int _numOfCurrentCombatRooms;
    [SerializeField] private Vector2 numOfBranchesRange;
    [SerializeField] public int _numOfPossibleBranches;
    private int _numOfCurrentBranches;

    public bool isMainRoute = true;

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
    public float savedWattime;

    public bool manualGeneration = false;
    

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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            waittime = savedWattime;
            if (gameObject.transform.childCount != 0)
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

    public void StartGeneration()
    {
        SpawnRoom(BasicRoom, firstRoomSpawn);
        if (isMainRoute)
        {
            //Sätter startrummet som currentroom så man spawnar på rätt rum
            partyHandler.currentRoom = currentRoom.GetComponent<Room>();
        }

        if (_numOfCurrentBasicRooms < _numOfPossibleBasicRooms && !manualGeneration)
        {
            StartCoroutine(GenerateNextRoom());
        }
    }

    public void SetGenerationValues()
    {
        _numOfCurrentBasicRooms = 0;
        _numOfCurrentCombatRooms = 0;

        if (isMainRoute)
        {
            _numOfPossibleBasicRooms = Mathf.RoundToInt(Random.Range(numOfBasicRoomsRange.x, numOfBasicRoomsRange.y));
            _numOfPossibleCombatRooms = Mathf.RoundToInt(Random.Range(numOfCombatRoomsRange.x, numOfCombatRoomsRange.y));
            _numOfPossibleBranches = Mathf.RoundToInt(Random.Range(numOfBranchesRange.x, numOfBranchesRange.y));
        }

        if (numOfBasicRoomsRange.x < 1)
        {
            _numOfPossibleBasicRooms = 1;
        }

        roomsUntilBranch = (int)_numOfPossibleBasicRooms / 3;

    }

    private void SetEventList()
    {
        int eventCount = (int)_numOfCurrentBasicRooms / 2;

    }

    private IEnumerator GenerateNextRoom()
    {
        yield return new WaitForSeconds(waittime);

        Vector3 nextRoomPosition = currentRoom.transform.position;
        // Sets the position of the next tile
        Vector3 nextRoomDirection = currentRoom.transform.position;
        nextRoomDirection.x += 10;
        nextRoomPosition = GetPositionOfNextRoom(2, nextRoomPosition);

        // Instantiates Room
        if(roomsUntilCombat <= 0)
        {
            SpawnRoom(CombatRoom, nextRoomPosition);
            roomsUntilCombat = 4;
        }
        else
        {
            SpawnRoom(BasicRoom, nextRoomPosition);
        }

        roomsUntilCombat--;
        roomsUntilBranch--;

        // FIXA SEN --------------------------------------
        //if (roomsUntilBranch <= 0 && isMainRoute && _numOfCurrentBranches < _numOfPossibleBranches)
        //{
        //    GenerateBranch();
        //}

        if(_numOfCurrentBasicRooms >= _numOfPossibleBasicRooms)
        {
            partyHandler.gameObject.SetActive(true);
        }
        else if (_numOfCurrentBasicRooms < _numOfPossibleBasicRooms)
        {
            if (!manualGeneration)
                StartCoroutine(GenerateNextRoom());
        }
    }

   
    private void SpawnRoom(GameObject prefab, Vector3 pos)
    {
        GameObject room = Instantiate(prefab, pos, Quaternion.identity);
        if (currentRoom != null)
        {
            Room previousRoomScript = currentRoom.GetComponent<Room>();
            Room currentRoomScript = room.GetComponent<Room>();

            if (currentRoomScript != null && previousRoomScript != null) {

                previousRoomScript.rightAdjacentRoom = currentRoomScript;
                currentRoomScript.leftAdjacentRoom = previousRoomScript;
 
            }

        }

        currentRoom = room;
        room.transform.SetParent(gameObject.transform, true);
        _numOfCurrentBasicRooms++;
    }

    int GetDirectionPosValue(int direction)
    {
        if (direction > 2)
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
