using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public TowerData data;

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
    }
    public void AddLevelRange()
    {
        rangeLevel++;
    }
    public void AddLevelNormal()
    {
        normalLevel++;
    }
    public void AddLevelArmor()
    {
        armorLevel++;
    }
    public void AddLevelmagicArmor()
    {
        magicArmorLevel++;
    }
    protected float CalculateDamageNormal(float dmg)
    {
        return dmg * (baseNormal + GameManager.gameData.normalMultiplier * normalLevel);
    }
    protected float CalculateDamageArmor(float dmg)
    {
        return dmg * (baseArmor + GameManager.gameData.armorMultiplier * armorLevel);
    }
    protected float CalculateDamageMagicArmor(float dmg)
    {
        return dmg * (baseMagicArmor + GameManager.gameData.magicArmorMultiplier * magicArmorLevel);
    }
}
