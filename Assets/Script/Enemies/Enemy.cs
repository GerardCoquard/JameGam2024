using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    float maxHealth;
    float health;
    float damage;
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
    void Start()
    {
        health = enemyData.health;
        damage = enemyData.damage;
        armor = enemyData.armor;
        magicArmor = enemyData.magicArmor;
        maxHealth = health + armor + magicArmor;
        currentHealth = maxHealth;
        currency = enemyData.currency;
        GetComponent<MoveEnemie>().speed = enemyData.speed;
        isAlive = true;

        uiDisplay.InitializeAll(health,armor,magicArmor, maxHealth, 10f);
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
            GameManager.AddCurrency(currency);
            UISpawner.instance.SpawnTextWithColor(transform.position,"+" + currency + "<sprite=0>",Color.white);
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
