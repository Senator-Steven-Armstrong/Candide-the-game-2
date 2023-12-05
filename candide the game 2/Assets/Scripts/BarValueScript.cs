using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class BarValueScript : MonoBehaviour
{
    public TextMesh text;
    private float prop;

    public void SetBarValue(float max, float curr)
    {
        prop = curr / max;
        transform.localScale = new Vector3(prop, 0.1f, 1);
        transform.localPosition = new Vector3(prop * 0.5f - 0.5f, 0, 0);
    }

    public void SetText(float curr, float max)
    {
        text.text = curr.ToString() + "/" + max.ToString();
    }

}
