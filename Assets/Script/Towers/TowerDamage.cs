using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamage : Tower
{    
    IEnumerator MoveBullet(float distance,Transform enemy)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.identity);
        audioSourceAction.PlayOneShot(audioSourceAction.clip);
        ParticleSystem particleSystem = bullet.GetComponent<ParticleSystem>();
        Vector3 startPos = bullet.transform.position;
        float time = 0;
        while (time < 1)
        {

            time += Time.deltaTime;
            float percentageDuration = time / 1;
            if (enemy != null)
            {
                //bullet.transform.position = Vector3.Lerp(startPos, enemy.position, bulletCurve.Evaluate(percentageDuration));
            }
            else
            {
                time = 1;
            }
            
            yield return new WaitForEndOfFrame();
        }
        if(enemy != null)
        {
            enemy.GetComponent<Enemy>().DamageEnemy(this, damage);
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            yield return new WaitForSeconds(5);
        }

        Destroy(bullet.gameObject);
    }
}
