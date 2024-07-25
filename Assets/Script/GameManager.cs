using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static GameData gameData;
    public static Dictionary<string, TowerData> towerData;
    public static int currency;
    public static Action<int> OnCurrencyChanged;
    static GameManager()
    {
        gameData = Resources.Load("GameData") as GameData;
        towerData = new Dictionary<string, TowerData>();
        foreach (TowerData tower in gameData.towers)
        {
            towerData.Add(tower.name,tower);
        }
    }
    
    public static void Reset()
    {
        currency = 0;
        OnCurrencyChanged = null;
    }

    public static void AddCurrency(int amount)
    {
        currency += amount;
        OnCurrencyChanged?.Invoke(currency);
    }
    
    public static void RemoveCurrency(int amount)
    {
        currency -= amount;
        OnCurrencyChanged?.Invoke(currency);
    }

    public static bool HaveCurrency(int amount)
    {
        return currency >= amount;
    }

    public static int GetCurrency()
    {
        return currency;
    }
}
