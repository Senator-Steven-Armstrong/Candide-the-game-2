using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHitboxScript : MonoBehaviour
{
    private BattleMenuCanvasScript canvasScript;

    private void OnMouseDown()
    {
        gameObject.GetComponent<BaseEntityScipt>().selectArrow.gameObject.GetComponent<BopScript>().isBoppin = true;

        canvasScript = GameObject.Find("Battle Menu Canvas").GetComponent<BattleMenuCanvasScript>();
        canvasScript.CheckSelectedEntity(gameObject);
    }
}
