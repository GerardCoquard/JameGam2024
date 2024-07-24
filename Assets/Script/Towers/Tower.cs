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
    public int rangeLevel = 0;
    public int fireRateLevel = 0;
    public float baseDamage;
    protected float baseNormal;
    protected float baseArmor;
    protected float baseMagicArmor;
    public int normalLevel = 0;
    public int armorLevel = 0;
    public int magicArmorLevel = 0;
    protected float actionRate;

    public bool hasChangedRange = false;
    protected float anteriorRange;

    //variables definitivas
    public float fireRate;
    public float range;
    public float normalPenetration;
    public float armorPenetration;
    public float magicArmorPenetration;

    public TowerData data;
    public Action OnStatChanged;

    protected List<Transform> enemyList;
    protected Transform enemy;
    Transform mage;

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
        enemyList = new List<Transform>();

        GetComponent<CapsuleCollider>().radius = range;
        mage = transform.GetChild(0);
    }
    private void Update()
    {

    }
    public virtual void Action()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
                i--;
            }
        }
        if (enemyList.Count != 0)
        {
            enemy = enemyList[0];
            Vector3 targetDirection = enemy.position - mage.position;
            targetDirection.y = 0;
            targetDirection.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            mage.rotation = targetRotation;
        }
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
        anteriorRange = range;
        range = baseRange + GameManager.gameData.rangeMultiplier * rangeLevel;
        GetComponent<CapsuleCollider>().radius = range;
        OnStatChanged?.Invoke();
        hasChangedRange = true;
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
    public float CalculateDamageNormal(float dmg)
    {
        return dmg * normalPenetration;
    }
    public float CalculateDamageArmor(float dmg)
    {
        return dmg * armorPenetration;
    }
    public float CalculateDamageMagicArmor(float dmg)
    {
        return dmg * magicArmorPenetration;
    }


    public float ArmorToBase(float dmg)
    {
        return dmg / armorPenetration;
    }
    public float MagicArmorToBase(float dmg)
    {
        return dmg / magicArmorPenetration;
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyList.Add(other.transform.parent);
    }
    private void OnTriggerExit(Collider other)
    {
        enemyList.Remove(other.transform.parent);
    }
}
