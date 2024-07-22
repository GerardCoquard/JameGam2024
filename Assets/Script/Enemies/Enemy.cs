using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    float health;
    int damage;
    private void Awake()
    {
        health = enemyData.health;
        damage = enemyData.damage;
    }
    public void DamageEnemy(float hitDamage)
    {
        health -= hitDamage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
