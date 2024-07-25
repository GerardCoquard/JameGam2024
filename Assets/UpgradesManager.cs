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
        _healthUpgrade.SetData(tower.normalLevel,"+" + _gameData.normalMultiplier.ToString("F1"),CalculatePrice(_gameData.upgradeBasePriceNormal,tower.normalLevel));
        _armorUpgrade.SetData(tower.armorLevel,"+" + _gameData.armorMultiplier.ToString("F1"),CalculatePrice(_gameData.upgradeBasePriceArmor,tower.armorLevel));
        _magicUpgrade.SetData(tower.magicArmorLevel,"+" + _gameData.magicArmorMultiplier.ToString("F1"),CalculatePrice(_gameData.upgradeBasePriceMagicArmor,tower.magicArmorLevel));
        _fireRateUpgrade.SetData(tower.fireRateLevel,"+" + (int)(_gameData.fireRateMultiplier*100)+"%",CalculatePrice(_gameData.upgradeBasePriceFireRate,tower.fireRateLevel));
        _rangeUpgrade.SetData(tower.rangeLevel,"+" + (int)(_gameData.rangeMultiplier*100)+"%",CalculatePrice(_gameData.upgradeBasePriceFireRate,tower.fireRateLevel));

        if (tower.data.towerName == "Purifier")
        {
            _healthUpgrade.gameObject.SetActive(false);
            _armorUpgrade.gameObject.SetActive(false);
            _magicUpgrade.gameObject.SetActive(false);
            _fireRateUpgrade.gameObject.SetActive(false);
        }
        else
        {
            _healthUpgrade.gameObject.SetActive(true);
            _armorUpgrade.gameObject.SetActive(true);
            _magicUpgrade.gameObject.SetActive(true);
            _fireRateUpgrade.gameObject.SetActive(true);
        }
    }

    private int CalculatePrice(int basePrice, int levels)
    {
        return (int)(basePrice + (basePrice * levels * _gameData.priceMultiplier));
    }
}
