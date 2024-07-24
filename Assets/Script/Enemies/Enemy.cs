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
        isAlive = true;

        uiDisplay.InitializeAll(health,armor,magicArmor, maxHealth, 10f);
    }
    public void DamageEnemy(float damage, out float damageRestante, float damageArmor, float damageMagicArmor)
    {
        damageRestante = 0;
        if (!isAlive) return;

        float previousHealth = currentHealth;
        if (magicArmor != 0)
        {
            currentHealth = Mathf.Clamp(currentHealth - damageMagicArmor, 0, maxHealth);
            if(health + armor >= currentHealth)
            {
                damageRestante = (health + armor) - currentHealth;
                magicArmor = 0;
                currentHealth = health + armor;
                UIUpdateIComprovar(previousHealth);
                return;
            }
            UIUpdateIComprovar(previousHealth);
            return;
        }
        else if(armor != 0)
        {
            currentHealth = Mathf.Clamp(currentHealth - damageArmor, 0, maxHealth);
            if (health >= currentHealth)
            {
                damageRestante = (health) - currentHealth;
                armor = 0;
                currentHealth = health;
                UIUpdateIComprovar(previousHealth);
                return;
            }
            UIUpdateIComprovar(previousHealth);
            return;
        }
        else
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
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
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
