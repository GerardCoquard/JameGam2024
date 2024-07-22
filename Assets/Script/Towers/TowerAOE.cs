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
            bullet.GetComponent<ActivateAreaDamage>().damage = damage;
            timer = 0;
            StartCoroutine(FollowEnemy(bullet.transform, enemy.transform));
            Destroy(bullet, 3);
        }
        timer += Time.deltaTime;
    }
    IEnumerator FollowEnemy(Transform bullet,Transform enemy)
    {
        float time = 0;
        MoveEnemie enemyMove = enemy.GetComponent<MoveEnemie>();
        while (time < 3)
        {
            time += Time.deltaTime;
            bullet.position = enemyMove.publicPos;
            yield return new WaitForEndOfFrame();
        }
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
