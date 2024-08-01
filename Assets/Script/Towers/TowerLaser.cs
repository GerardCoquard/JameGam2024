using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLaser : Tower
{
    float timer;
    [SerializeField]Transform bulletInstantiatePoint;
    [SerializeField] AnimationCurve laserCurve;
    bool freezeRotation = false;

    private ParticleSystem particleSystem;

    private void Awake()
    {      
        timer = 0;
    }
    // public override void Action()
    // {
    //     if(target != null)
    //     {           
    //         float distance = Vector3.Distance(target.position, bulletInstantiatePoint.position);
    //         if (distance <= range && timer >= 1 / fireRate)
    //         {
    //             GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.identity);
    //             bullet.transform.GetChild(0).GetChild(0).GetComponent<ActivateAreaDamage>().SetVariables(damage, this);
    //             audioSourceAction.PlayOneShot(audioSourceAction.clip);
    //             animator.SetBool("laser", true);
    //             bullet.transform.parent = bulletInstantiatePoint;
    //             bullet.transform.localScale = new Vector3(1, 1, 1);
    //             timer = 0;
    //             StartCoroutine(MoveBullet(distance, target, bullet.transform));
    //         }
    //     }
    //     
    //     timer += Time.deltaTime;
    //
    // }
    
    IEnumerator MoveBullet(float distance,Transform enemy, Transform bullet)
    {

        //bullet.GetChild(0).GetChild(0).GetComponent<ActivateAreaDamage>().damage = baseDamage;
        Vector3 startRot = new Vector3(-80,180, 0);
        Vector3 endRot = new Vector3(20,180,0);
        float time = 0;
        while (time < 1.5f)
        {
            time += Time.deltaTime;
            float percentageDuration = time / 1.5f;
            bullet.localEulerAngles = Vector3.Lerp(startRot, endRot, laserCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
        freezeRotation = false;
        animator.SetBool("laser", false);
        bullet.GetComponent<Animator>().SetTrigger("despawn");
        yield return new WaitForSeconds(1);
        Destroy(bullet.gameObject);
    }
}
