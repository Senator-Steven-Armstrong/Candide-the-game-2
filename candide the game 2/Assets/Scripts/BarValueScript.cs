using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarValueScript : MonoBehaviour
{
    public float max;
    public float curr;
    private float prop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        prop = curr / max;
        transform.localScale = new Vector3(prop, 0.1f, 1);
        transform.localPosition = new Vector3(prop * 0.5f - 0.5f, 0, 0);
    }
}
