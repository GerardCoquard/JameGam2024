using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamage : Tower
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

    Transform bulletInstantiatePoint;

    private void Awake()
    {      
        fireRate = data.fireRate;
        range = data.range;
        startEffect = data.startEffect;
        bulletPrefab = data.bullet;
        endEffect = data.endEffect;

        health = maxHealth;

        timer = 0;
        bulletInstantiatePoint = transform.GetChild(0);
    }
    public override void StartTower()
    {
        base.StartTower();
    }
    public override void Action()
    {
        base.Action();
        float distance = Vector3.Distance(enemy.position, bulletInstantiatePoint.position);
        if (distance <= range && timer >= fireRate)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().enemy = enemy;
            timer = 0;           
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
