using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static GameData gameData;
    public static Dictionary<string, TowerData> towerData;
    static GameManager()
    {
        gameData = Resources.Load("GameData") as GameData;
        towerData = new Dictionary<string, TowerData>();
        foreach (TowerData tower in gameData.towers)
        {
            towerData.Add(tower.name,tower);
        }
    }
}
