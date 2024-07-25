using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameData", menuName = "GameData", order = 1)]
public class GameData : ScriptableObject
{
    public List<TowerData> towers;
    [Header("Price Related")]
    public int startingCurrency;
    public float priceMultiplier;
    public int upgradeBasePriceNormal;
    public int upgradeBasePriceArmor;
    public int upgradeBasePriceMagicArmor;
    public int upgradeBasePriceFireRate;
    public int upgradeBasePriceRange;
    [Header("Upgrade Related")]
    public float normalMultiplier;
    public float armorMultiplier;
    public float magicArmorMultiplier; 
    public float fireRateMultiplier;
    public float rangeMultiplier;
    [Header("Enemies Related")]
    public float spawnTimeBetweenEnemies;
    public float enemyStatsMultiplier;
    public int healthSegmentAmount;
    
}
