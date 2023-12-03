using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatRoomScript : Room
{
    public GameObject specificBattleScene = null;
    public List<GameObject> enemiesToFight = new List<GameObject>();   

    public override IEnumerator StartCombat(float waitSeconds)
    {
        GameHandlerScript gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandlerScript>();
        if (specificBattleScene != null)
        {
            gameHandler.currentCombatEnviornment = specificBattleScene;
        }
        else
        {
            gameHandler.currentCombatEnviornment = gameHandler.baseCombatEnviornment;
        }
        gameHandler.enemiesInCombat = enemiesToFight;

        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene(2);

    }
}
