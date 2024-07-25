using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float maxHealth;
    float health;
    int damage;
    float armor;
    float magicArmor;
    bool isAlive;
    float currentHealth;
    int currency;
    [HideInInspector]
    public float speed;
    //Events
    public delegate void SetHealth(float health, float maxHealth, float _segmentValue);
    public delegate void HealthChanged(float actualHealth, float previousHealth);
    public static event SetHealth OnSetHealth;
    public static event HealthChanged OnHealthChanged;
    [SerializeField] AmountDisplay uiDisplay;

    // Start is called before the first frame update

    public void InitializeEnemy(int health, int armor, int magic, int currency, EnemyData enemyData)
    {
        this.health = health;
        this.armor = armor;
        this.magicArmor = magic;
        this.currency = currency;
        damage = enemyData.damage;
        maxHealth = health + armor + magicArmor;
        currentHealth = maxHealth;
        GetComponent<MoveEnemie>().speed = enemyData.speed;
        isAlive = true;

        uiDisplay.InitializeAll(health,armor,magicArmor, maxHealth, GameManager.gameData.healthSegmentAmount);
    }
    void Start()
    {
        
    }
    
    private void OnDestroy()
    {
        OnSetHealth = null;
        OnHealthChanged = null;
    }
    public void DamageEnemy(Tower tower,float dañoRestante)
    {            
        if (!isAlive) return;

        float previousHealth = currentHealth;
        if (magicArmor != 0)
        {
            int magicArmorDmg = Mathf.RoundToInt(tower.CalculateDamageMagicArmor(dañoRestante));            
            currentHealth = Mathf.Clamp(currentHealth - magicArmorDmg, 0, maxHealth);
            if(health + armor >= currentHealth)
            {
                dañoRestante = (health + armor) - currentHealth;
                magicArmor = 0;
                currentHealth = health + armor;
                UIUpdateIComprovar(previousHealth);
                dañoRestante = Mathf.RoundToInt(tower.MagicArmorToBase(dañoRestante));
                DamageEnemy(tower, dañoRestante);
                return;
            }
            UIUpdateIComprovar(previousHealth);
            return;
        }
        else if(armor != 0)
        {
            int armorDmg = Mathf.RoundToInt(tower.CalculateDamageArmor(dañoRestante));
            currentHealth = Mathf.Clamp(currentHealth - armorDmg, 0, maxHealth);
            if (health >= currentHealth)
            {
                dañoRestante = (health) - currentHealth;
                armor = 0;
                currentHealth = health;
                UIUpdateIComprovar(previousHealth);
                dañoRestante = Mathf.RoundToInt(tower.ArmorToBase(dañoRestante));
                DamageEnemy(tower, dañoRestante);
                return;
            }
            UIUpdateIComprovar(previousHealth);
            return;
        }
        else
        {
            int normalDmg = Mathf.RoundToInt(tower.CalculateDamageNormal(dañoRestante));
            currentHealth = Mathf.Clamp(currentHealth - normalDmg, 0, maxHealth);
            UIUpdateIComprovar(previousHealth);
        }
        
    }
    void UIUpdateIComprovar(float previousHealth)
    {
        //Update UI
        uiDisplay.SetFillsFollow(currentHealth, previousHealth);
        //Comprobación de posible final de partida
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
}
