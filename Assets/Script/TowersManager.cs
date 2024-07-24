using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class TowersManager
{
    private static Dictionary<string, TowerGlobalUpgrades> _towerGlobalUpgrades;
    public static Action OnStatsChange;
    public static Action OnPriceChange;

    static TowersManager()
    {
        _towerGlobalUpgrades = new Dictionary<string, TowerGlobalUpgrades>();
        
    }

    public static Dictionary<string, TowerGlobalUpgrades> GetUpgrades()
    {
        return _towerGlobalUpgrades;
    }
}

