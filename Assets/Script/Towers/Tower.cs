using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [HideInInspector]
    //Final
    public float damage;
    public float normalPenetration;
    public float armorPenetration;
    public float magicArmorPenetration;
    public float fireRate;
    public float range;
    //Base
    protected float baseNormal;
    protected float baseArmor;
    protected float baseMagicArmor;
    protected float baseFireRate;
    protected float baseRange;
    //Levels
    public int normalLevel = 0;
    public int armorLevel = 0;
    public int magicArmorLevel = 0;
    public int rangeLevel = 0;
    public int fireRateLevel = 0;
    //Data
    public int basePrice;
    public string towerName;
    public Sprite icon;
    protected GameObject bulletPrefab;
    //BaseTowerVars
    protected float fireRateTimer;
    public Transform bulletInstantiatePoint;
    protected TargetSelector targetSelector;
    public float animationDelay;  
    
    public Action OnStatChanged;
    
    protected Transform target;
    Transform mage;
    public Animator animator;
    public AudioSource audioSource;
    bool isAlive = true;
    public virtual void StartTower(TowerData data)
    {
        audioSource.PlayOneShot(audioSource.clip);
        baseFireRate = data.fireRate;
        baseRange = data.range;
        bulletPrefab = data.bullet;
        damage = data.baseDamage;
        baseNormal = data.normalBase;
        baseArmor = data.armorBase;
        baseMagicArmor = data.magicArmorBase;
        basePrice = data.price;
        towerName = data.towerName;
        icon = data.icon;
        
        fireRate = baseFireRate - GameManager.gameData.fireRateMultiplier * fireRateLevel;
        range = baseRange + baseRange * GameManager.gameData.rangeMultiplier * rangeLevel;
        normalPenetration = baseNormal + GameManager.gameData.normalMultiplier * normalLevel;
        armorPenetration = baseArmor + GameManager.gameData.armorMultiplier * armorLevel;
        magicArmorPenetration = baseMagicArmor + GameManager.gameData.magicArmorMultiplier * magicArmorLevel;

        GetComponent<CapsuleCollider>().radius = range;
        targetSelector = GetComponent<TargetSelector>();
        targetSelector.Initialize();
        mage = transform.GetChild(0);
    }
    protected virtual void Update()
    {
        if(fireRateTimer < 1/fireRate)
            fireRateTimer += Time.deltaTime;
        
        target = targetSelector.GetTarget();
        
        if (target != null)
        {
            MageLookTarget();
            if (fireRateTimer >= 1/fireRate)
            {
                StartCoroutine(Fire());
                AnimationTrigger();
                fireRateTimer = 0;
            }
        }
    }

    protected virtual IEnumerator Fire()
    {
        yield return new WaitForSeconds(animationDelay);
        GameObject bullet = Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Action(this, target);
    }

    protected virtual void AnimationTrigger()
    {
        animator.SetTrigger(towerName);
    }
    

    protected virtual void MageLookTarget()
    {
        Vector3 targetDirection = target.position - mage.position;
        targetDirection.y = 0;
        targetDirection.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        mage.rotation = targetRotation;
    }

    public TargetSelector GetTargetSelector()
    {
        return targetSelector;
    }
    
    //Levels
    public void AddLevelFireRate()
    {
        fireRateLevel++;
        fireRate = baseFireRate + GameManager.gameData.fireRateMultiplier * fireRateLevel;
        OnStatChanged?.Invoke();
    }
    public virtual void AddLevelRange()
    {
        rangeLevel++;
        range = baseRange + baseRange * GameManager.gameData.rangeMultiplier * rangeLevel;
        GetComponent<CapsuleCollider>().radius = range;
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
    public void AddLevelMagicArmor()
    {
        magicArmorLevel++;
        magicArmorPenetration = baseMagicArmor + GameManager.gameData.magicArmorMultiplier * magicArmorLevel;
        OnStatChanged?.Invoke();
    }
    //Damage
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
}
