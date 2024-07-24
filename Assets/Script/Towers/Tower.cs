using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Tower : MonoBehaviour
{
    [HideInInspector]
    protected float baseFireRate;
    protected float baseRange;
    protected GameObject startEffect;
    protected GameObject bulletPrefab;
    protected GameObject endEffect;
    public float rangeLevel = 0;
    public float fireRateLevel = 0;
    protected float baseDamage;
    protected float baseNormal;
    protected float baseArmor;
    protected float baseMagicArmor;
    public float normalLevel = 0;
    public float armorLevel = 0;
    public float magicArmorLevel = 0;
    protected float actionRate;

    //variables definitivas
    public float fireRate;
    public float range;
    public float normalPenetration;
    public float armorPenetration;
    public float magicArmorPenetration;

    public TowerData data;

    public Action OnStatChanged;


    [HideInInspector]
    bool isAlive = true;
    public virtual void StartTower()
    {
        baseFireRate = data.fireRate;
        baseRange = data.range;
        startEffect = data.startEffect;
        bulletPrefab = data.bullet;
        endEffect = data.endEffect;
        baseDamage = data.baseDamage;
        baseNormal = data.normalBase;
        baseArmor = data.armorBase;
        baseMagicArmor = data.magicArmorBase;

        fireRate = baseFireRate - GameManager.gameData.attackSpeedMultiplier * fireRateLevel;
        range = baseRange + GameManager.gameData.rangeMultiplier * rangeLevel;
        normalPenetration = baseNormal + GameManager.gameData.normalMultiplier * normalLevel;
        armorPenetration = baseArmor + GameManager.gameData.armorMultiplier * armorLevel;
        magicArmorPenetration = baseMagicArmor + GameManager.gameData.magicArmorMultiplier * magicArmorLevel;
    }
    public virtual void Action()
    {

    }
    public virtual void EndTower()
    {

    }
    public void AddLevelFireRate()
    {
        fireRateLevel++;
        fireRate = baseFireRate - GameManager.gameData.attackSpeedMultiplier * fireRateLevel;
        OnStatChanged?.Invoke();
    }
    public void AddLevelRange()
    {
        rangeLevel++;
        range = baseRange + GameManager.gameData.rangeMultiplier * rangeLevel;
        OnStatChanged?.Invoke();
    }
    public void AddLevelNormal()
    {
        normalLevel++;
        normalPenetration = baseNormal + GameManager.gameData.normalMultiplier * normalLevel;
        OnStatChanged?.Invoke();
    }
    public void AddLevelArmor()
    {
        armorLevel++;
        armorPenetration = baseArmor + GameManager.gameData.armorMultiplier * armorLevel;
        OnStatChanged?.Invoke();
    }
    public void AddLevelmagicArmor()
    {
        magicArmorLevel++;
        magicArmorPenetration = baseMagicArmor + GameManager.gameData.magicArmorMultiplier * magicArmorLevel;
        OnStatChanged?.Invoke();
    }
    protected float CalculateDamageNormal(float dmg)
    {
        return dmg * normalPenetration;
    }
    protected float CalculateDamageArmor(float dmg)
    {
        return dmg * armorPenetration;
    }
    protected float CalculateDamageMagicArmor(float dmg)
    {
        return dmg * magicArmorPenetration;
    }


    protected float ArmorToBase(float dmg)
    {
        return dmg / armorPenetration;
    }
    protected float MagicArmorToBase(float dmg)
    {
        return dmg / magicArmorPenetration;
    }
}
