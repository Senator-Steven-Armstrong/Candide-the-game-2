using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyHandlerScript : MonoBehaviour
{

    public List<GameObject> PartyMembers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(PartyMembers[0], Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
