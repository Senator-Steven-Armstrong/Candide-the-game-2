using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class BarValueScript : MonoBehaviour
{

    private float prop;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBarValue(float max, float curr)
    {
        prop = curr / max;
        transform.localScale = new Vector3(prop, 0.1f, 1);
        transform.localPosition = new Vector3(prop * 0.5f - 0.5f, 0, 0);
    }
}
