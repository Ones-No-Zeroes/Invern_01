using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyInvernLogic : MonoBehaviour
{   
    private LeverEnemy[] leverEnemies;

    void Awake()
    {
        leverEnemies = FindObjectsByType<LeverEnemy>(FindObjectsSortMode.None);
    }

    /// <summary>
    /// Iterates through all lever enemies to set if it is the LightMode or the VoidMode version of the enemy.
    /// </summary>
    /// <param name="worldVoid"></param>
    public void EnemyWorldState(bool worldVoid)
    {
        if (worldVoid)
        {
            foreach(LeverEnemy leverEnemy in leverEnemies)
            {
                leverEnemy.LightModeAssets.SetActive(false);
                leverEnemy.VoidModeAssets.SetActive(true);
            }
        }
        else
        {
            foreach(LeverEnemy leverEnemy in leverEnemies)
            {
                leverEnemy.LightModeAssets.SetActive(true);
                leverEnemy.VoidModeAssets.SetActive(false);
            }
        }
    }
}
