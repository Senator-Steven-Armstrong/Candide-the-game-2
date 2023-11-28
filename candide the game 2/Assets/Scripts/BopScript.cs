using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BopScript : MonoBehaviour
{
    public bool isBoppin = false;
    private Vector3 position;
    private Vector3 startPosition;

    private void Start()
    {
        isBoppin = false;
        position = transform.position;
        startPosition = transform.position; 
    }

    private void Update()
    {
        if (isBoppin)
        {
            position.y = 0.08f * Mathf.Sin(7 * Time.time) + startPosition.y;
            transform.position = position;
        }
    }
}
