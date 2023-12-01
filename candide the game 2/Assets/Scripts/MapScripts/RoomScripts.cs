using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector3 movementPosition;
    public Room topAdjacentRoom;
    public Room bottomAdjacentRoom;
    public Room rightAdjacentRoom;
    public Room leftAdjacentRoom;
    public bool isSecret;

    private void Start()
    {
        movementPosition = transform.position;
    }
}
