using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLaser : Tower
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
    [SerializeField] AnimationCurve laserCurve;

    private ParticleSystem particleSystem;

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
        bulletInstantiatePoint = transform.GetChild(0);
    }
    public override void StartTower()
    {
        base.StartTower();
    }
    public override void Action()
    {
        base.Action();
        transform.forward = (enemy.position - transform.position).normalized;
        float distance = Vector3.Distance(enemy.position, bulletInstantiatePoint.position);
        if (distance <= range && timer >= fireRate)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.identity);
            timer = 0;
            StartCoroutine(MoveBullet(distance, enemy, bullet.transform));
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
    IEnumerator MoveBullet(float distance,Transform enemy, Transform bullet)
    {
        bullet.rotation = Quaternion.LookRotation((enemy.position - bullet.position).normalized);
        Vector3 startRot = new Vector3(80, bullet.localEulerAngles.y, bullet.localEulerAngles.z);
        Vector3 endRot = new Vector3(-20, bullet.localEulerAngles.y, bullet.localEulerAngles.z);
        float time = 0;
        while (time < 3)
        {
            time += Time.deltaTime;
            float percentageDuration = time / 3;
            bullet.rotation = Quaternion.Lerp(Quaternion.Euler(startRot), Quaternion.Euler(endRot), laserCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
        //enemy.GetComponent<Enemy>().DamageEnemy(damage);
        //particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitForSeconds(5);
        Destroy(bullet.gameObject);
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
