using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamage : Tower
{    
    [SerializeField] Transform enemy;
    float timer;
    [SerializeField] AnimationCurve bulletSpeedCurve;
    [SerializeField] float bulletSpeedMultiplier;
    Transform bulletInstantiatePoint;

    private ParticleSystem particleSystem;

    private void Awake()
    {      
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
        if(enemy != null)
        {
            float distance = Vector3.Distance(enemy.position, bulletInstantiatePoint.position);
            if (distance <= range && timer >= fireRate)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.identity);
                particleSystem = bullet.GetComponent<ParticleSystem>();
                timer = 0;
                StartCoroutine(MoveBullet(distance, enemy, bullet.transform));
            }
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
        Vector3 startPos = bullet.position;
        float time = 0;
        float targetTime = distance / bulletSpeedMultiplier;
        while (time < 1)
        {
            if(enemy == null)
            {
                Destroy(bullet.gameObject);
                yield return null;
            }
            time += Time.deltaTime;
            float percentageDuration = time / 1;
            bullet.position = Vector3.Lerp(startPos, enemy.position, bulletSpeedCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
        enemy.GetComponent<Enemy>().DamageEnemy(this,baseDamage);      

        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitForSeconds(5);
        Destroy(bullet.gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {
        Action();
    }
}
