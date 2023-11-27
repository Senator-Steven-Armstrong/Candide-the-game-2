using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityButtonScript : MonoBehaviour
{
    public Text stringName;
    public Sprite portrait;
    public Image portraitImage;
    public GameObject entity;

    private void Start()
    {

        portraitImage = GetComponent<Image>();
    }

    public void SetVariables()
    {
        BaseEntityScipt entitityScript = entity.GetComponent<BaseEntityScipt>();
        Debug.Log(stringName);
        Debug.Log(entitityScript);
        stringName.text = entitityScript.stringName;
        portrait = entitityScript.portrait;
        if(portrait != null )
            portraitImage.sprite = portrait;
    }
}
