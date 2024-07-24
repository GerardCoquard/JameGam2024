using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private Upgrade _healthUpgrade;
    [SerializeField] private Upgrade _armorUpgrade;
    [SerializeField] private Upgrade _magicUpgrade;
    [SerializeField] private Upgrade _fireRateUpgrade;
    [SerializeField] private Upgrade _rangeUpgrade;

    private GameData _gameData;

    private void Awake()
    {
        _gameData = GameManager.gameData;
    }

    public void SetData(Tower tower)
    {
        //_healthUpgrade.SetData(CalculatePrice(_gameData.upgradeBasePriceNormal,tower.normalLevel,_gameData.priceMultiplier));
    }

    private int CalculatePrice(int basePrice, int levels, float percentageIncrement)
    {
        return (int)(basePrice + (basePrice * levels * percentageIncrement));
    }
}
