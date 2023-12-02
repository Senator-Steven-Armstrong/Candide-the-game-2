using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Vector3 posOffset;
    public GameObject objectToFollow;

    // Start is called before the first frame update
    void Start()
    {
         posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = objectToFollow.gameObject.transform.position + posOffset;
    }
}
