using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameData", order = 1)]
public class GameData : ScriptableObject
{
    public List<TowerData> towers;
    public float priceMultiplier;
    public int upgradeBasePriceNormal;
    public int upgradeBasePriceArmor;
    public int upgradeBasePriceMagicArmor;
    public int upgradeBasePriceFireRate;
    public int upgradeBasePriceRange;
    public float fireRateMultiplier;
    public float rangeMultiplier;
    
    public float normalMultiplier;
    public float armorMultiplier;
    public float magicArmorMultiplier;
    public float spawnTimeBetweenEnemys;
    public float enemyStatsMultiplier;
    public int healthSegmentAmount;
    public int startingCurrency;
}
