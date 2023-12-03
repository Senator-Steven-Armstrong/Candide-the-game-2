using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    public Vector3 movementPosition;
    public Room topAdjacentRoom;
    public Room bottomAdjacentRoom;
    public Room rightAdjacentRoom;
    public Room leftAdjacentRoom;
    public bool isSecret;
    public bool isCombatRoom;

    private void Start()
    {
        movementPosition = transform.position;
    }

    public virtual IEnumerator StartCombat(float waitSeconds)
    {
        yield return null;

    }
}
