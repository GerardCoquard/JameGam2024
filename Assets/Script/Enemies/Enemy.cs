using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int maxHealth;
    int health;
    int damage;
    int armor;
    int magicArmor;
    bool isAlive;
    int currentHealth;
    int currency;
    [HideInInspector]
    public float speed;
    //Events
    public delegate void SetHealth(float health, float maxHealth, float _segmentValue);
    public delegate void HealthChanged(float actualHealth, float previousHealth);
    public static event SetHealth OnSetHealth;
    public static event HealthChanged OnHealthChanged;
    [SerializeField] AmountDisplay uiDisplay;
    private MoveEnemy _moveEnemy;

    // Start is called before the first frame update

    public void InitializeEnemy(int health, int armor, int magic, int currency, EnemyData enemyData)
    {
        this.health = health;
        this.armor = armor;
        this.magicArmor = magic;
        this.currency = currency;
        damage = enemyData.damage;
        maxHealth = this.health + this.armor + this.magicArmor;
        currentHealth = maxHealth;
        GetComponent<MoveEnemy>().speed = enemyData.speed;
        isAlive = true;
        _moveEnemy = GetComponent<MoveEnemy>();
        uiDisplay.InitializeAll(health,armor,magicArmor, maxHealth, GameManager.gameData.healthSegmentAmount);
    }
    
    private void OnDestroy()
    {
        OnSetHealth = null;
        OnHealthChanged = null;
    }
    public void DamageEnemyDeprecated(Tower tower,float damageReceived)
    {            
        if (!isAlive) return;

        float previousHealth = currentHealth;
        if (magicArmor != 0)
        {
            int magicArmorDmg = Mathf.RoundToInt(tower.CalculateDamageMagicArmor(damageReceived));
            currentHealth = Mathf.Clamp(currentHealth - magicArmorDmg, 0, maxHealth);
            if(health + armor >= currentHealth)
            {
                damageReceived = (health + armor) - currentHealth;
                magicArmor = 0;
                currentHealth = health + armor;
                //CheckAlive(previousHealth);
                damageReceived = Mathf.RoundToInt(tower.MagicArmorToBase(damageReceived));
                DamageEnemy(tower, damageReceived);
                return;
            }
            //CheckAlive(previousHealth);
            return;
        }
        else if(armor != 0)
        {
            int armorDmg = Mathf.RoundToInt(tower.CalculateDamageArmor(damageReceived));
            currentHealth = Mathf.Clamp(currentHealth - armorDmg, 0, maxHealth);
            if (health >= currentHealth)
            {
                damageReceived = (health) - currentHealth;
                armor = 0;
                currentHealth = health;
                //CheckAlive(previousHealth);
                damageReceived = Mathf.RoundToInt(tower.ArmorToBase(damageReceived));
                DamageEnemy(tower, damageReceived);
                return;
            }
            //CheckAlive(previousHealth);
            return;
        }
        else
        {
            int normalDmg = Mathf.RoundToInt(tower.CalculateDamageNormal(damageReceived));
            currentHealth = Mathf.Clamp(currentHealth - normalDmg, 0, maxHealth);
            //CheckAlive(previousHealth);
        }
        
    }
    public void DamageEnemy(Tower tower,float damageReceived)
    {            
        if (!isAlive) return;

        float previousHealth = currentHealth;
        int excess;

        if (magicArmor > 0)
        {
            int magicArmorDmg = Mathf.RoundToInt(tower.CalculateDamageMagicArmor(damageReceived));
            excess = magicArmorDmg - magicArmor;
            magicArmor = Mathf.Clamp(magicArmor - magicArmorDmg, 0, magicArmor);
            damageReceived = Mathf.Clamp(tower.MagicArmorToBase(excess),0,damageReceived);
        }

        if (armor > 0 && damageReceived > 0)
        {
            int armorDmg = Mathf.RoundToInt(tower.CalculateDamageArmor(damageReceived));
            excess = armorDmg - armor;
            armor = Mathf.Clamp(armor - armorDmg, 0, armor);
            damageReceived = Mathf.Clamp(tower.ArmorToBase(excess),0,damageReceived);
        }
        
        if (health > 0 && damageReceived > 0)
        {
            int healthDmg = Mathf.RoundToInt(tower.CalculateDamageNormal(damageReceived));
            health = Mathf.Clamp(health - healthDmg, 0, health);
        }

        currentHealth = health + armor + magicArmor;
        uiDisplay.SetFillsFollow(currentHealth, previousHealth);
        CheckAlive();
    }
    
    void CheckAlive()
    {
        isAlive = currentHealth > 0;
        if (!isAlive)
        {
            GetKill();
            Die();
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    private void GetKill()
    {
        GameManager.AddCurrency(currency);
        UISpawner.instance.SpawnTextWithColor(transform.position,"+" + currency + "<sprite=0>",Color.white);
    }

    public void Die()
    {
        WaveManager.instance.CheckIfWaveEnded();
        Destroy(gameObject);
    }

    public float GetPathDistance()
    {
        return _moveEnemy.distanceToEnd;
    }

    public float GetHealth()
    {
        return health;
    }
    
    public float GetArmor()
    {
        return armor;
    }
    
    public float GetMagic()
    {
        return magicArmor;
    }
}
