using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHitboxScript : MonoBehaviour
{
    private BattleMenuCanvasScript canvasScript;


    private void OnMouseDown()
    {
        canvasScript = GameObject.Find("Battle Menu Canvas").GetComponent<BattleMenuCanvasScript>();
        canvasScript.dosometing(gameObject);
    }
}
