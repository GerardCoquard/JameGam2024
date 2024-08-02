using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Bullet
{
    [SerializeField] AnimationCurve bulletCurve;
    private void Start()
    {
        StartCoroutine(MoveBullet());
    }
    IEnumerator MoveBullet()
    {
        yield return new WaitForSeconds(0.2f);        
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Vector3 startPos = transform.position;
        float time = 0;
        while (time < 1)
        {

            time += Time.deltaTime;
            float percentageDuration = time / 1;
            transform.position = Vector3.Lerp(startPos, enemyPos, bulletCurve.Evaluate(percentageDuration));           
            yield return new WaitForEndOfFrame();
        }
        if(enemy != null)
        {
            enemy.GetComponent<Enemy>().DamageEnemy(tower, tower.damage);
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            yield return new WaitForSeconds(5);
        }
        Destroy(gameObject);
    }
}
