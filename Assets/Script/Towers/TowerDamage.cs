using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamage : Tower
{    
    float timer;
    [SerializeField] AnimationCurve bulletSpeedCurve;
    [SerializeField] float bulletSpeedMultiplier;
    [SerializeField] Transform bulletInstantiatePoint;

    private ParticleSystem particleSystem;

    private void Awake()
    {      
        timer = 0;
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
            if (distance <= range && timer >= 1 / fireRate)
            {               
                timer = 0;
                animator.SetTrigger("projectile");
                StartCoroutine(MoveBullet(distance, enemy));
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
    IEnumerator MoveBullet(float distance,Transform enemy)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.identity);
        particleSystem = bullet.GetComponent<ParticleSystem>();
        Vector3 startPos = bullet.transform.position;
        float time = 0;
        float targetTime = distance / bulletSpeedMultiplier;
        while (time < 1)
        {

            time += Time.deltaTime;
            float percentageDuration = time / 1;
            if (enemy != null)
            {
                bullet.transform.position = Vector3.Lerp(startPos, enemy.position, bulletSpeedCurve.Evaluate(percentageDuration));
            }
            else
            {
                time = 1;
            }
            
            yield return new WaitForEndOfFrame();
        }
        if(enemy != null)
        {
            enemy.GetComponent<Enemy>().DamageEnemy(this, baseDamage);
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            yield return new WaitForSeconds(5);
        }

        Destroy(bullet.gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {
        Action();
    }
}
