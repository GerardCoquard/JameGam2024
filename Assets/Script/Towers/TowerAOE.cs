using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAOE : Tower
{
    [SerializeField] TowerData data;
    float fireRate;
    int damage;
    float range;
    GameObject startEffect;
    GameObject bulletPrefab;
    GameObject endEffect;
    [SerializeField] Transform enemy;
    float timer;

    private void Awake()
    {      
        fireRate = data.fireRate;
        range = data.range;
        startEffect = data.startEffect;
        bulletPrefab = data.bullet;
        endEffect = data.endEffect;
        damage = data.damage;
        health = maxHealth;

        timer = 0;
    }
    public override void StartTower()
    {
        base.StartTower();
    }
    public override void Action()
    {
        base.Action();
        float distance = Vector3.Distance(enemy.position, transform.position);
        if (distance <= range && timer >= fireRate)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, enemy.position, Quaternion.identity);
            timer = 0;
            Destroy(bullet, 4);
        }
        timer += Time.deltaTime;
    }
    
    public override void EndTower()
    {
        base.EndTower();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartTower();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        Action();
        if(health<= 0)
        {
            EndTower();
        }
    }
}
