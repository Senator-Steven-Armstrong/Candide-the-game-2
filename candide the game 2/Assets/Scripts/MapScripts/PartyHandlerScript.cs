using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyHandlerScript : MonoBehaviour
{
    public GameHandlerScript gameHandler;
    public GameObject candidePref;
    public List<GameObject> PartyMembers = new List<GameObject>();
    public Room currentRoom;
    private Vector3 _currentPosition;
    private Vector3 _targetPosition;

    private float _timeElapsed;
    public float moveTime;

    public MapHandler mapHandler;

    public bool isMoving;

    public enum MoveStates
    {
        WAIT,
        MOVING
    }

    public MoveStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandlerScript>();
        AddEntityToParty(candidePref);
        GameObject candide = Instantiate(PartyMembers[0], Vector3.zero, Quaternion.identity);
        candide.transform.SetParent(transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (currentRoom.topAdjacentRoom)
                {
                    _targetPosition = currentRoom.topAdjacentRoom.movementPosition;
                    currentRoom = currentRoom.topAdjacentRoom;
                    currentState = MoveStates.MOVING;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (currentRoom.bottomAdjacentRoom)
                {
                    _targetPosition = currentRoom.bottomAdjacentRoom.movementPosition;
                    currentRoom = currentRoom.bottomAdjacentRoom;
                    currentState = MoveStates.MOVING;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentRoom.leftAdjacentRoom)
                {
                    _targetPosition = currentRoom.leftAdjacentRoom.movementPosition;
                    currentRoom = currentRoom.leftAdjacentRoom;
                    currentState = MoveStates.MOVING;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentRoom.rightAdjacentRoom)
                {
                    _targetPosition = currentRoom.rightAdjacentRoom.movementPosition;
                    currentRoom = currentRoom.rightAdjacentRoom;
                    currentState = MoveStates.MOVING;
                }
            }
        } 

        switch (currentState)
        {
            case MoveStates.WAIT:
                isMoving = false;
                _timeElapsed = 0;
                break;
            case MoveStates.MOVING:
                isMoving = true;
                _timeElapsed += Time.deltaTime;
                float percentageComplete = _timeElapsed / moveTime;
                gameObject.transform.position = Vector3.Lerp(_currentPosition, _targetPosition, percentageComplete);

                if(gameObject.transform.position == _targetPosition)
                {

                    StartCoroutine(currentRoom.StartCombat(1));

                    _currentPosition = _targetPosition;
                    currentState = MoveStates.WAIT;
                    
                }
                break;
            default:
                break;
        }

    }

    public void AddEntityToParty(GameObject MapEntity)
    {
        PartyMembers.Add(MapEntity);
        gameHandler.PartyMembers.Add(MapEntity.GetComponent<MapEntityRepScript>().representedPrefab);
    }
}
