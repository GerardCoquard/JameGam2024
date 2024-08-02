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

    //private void Awake()
    //{      
    //    timer = 0;
    //}
    //public override void Action()
    //{
    //    if (target != null)
    //    {
    //        float distance = Vector3.Distance(target.position, bulletInstantiatePoint.position);
    //        if (distance <= range && timer >= 1 / fireRate)
    //        {
    //            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.identity);
    //            bullet.transform.GetChild(0).GetChild(0).GetComponent<ActivateAreaDamage>().SetVariables(damage, this);
    //            audioSourceAction.PlayOneShot(audioSourceAction.clip);
    //            animator.SetBool("laser", true);
    //            bullet.transform.parent = bulletInstantiatePoint;
    //            bullet.transform.localScale = new Vector3(1, 1, 1);
    //            timer = 0;
    //            StartCoroutine(MoveBullet(distance, target, bullet.transform));
    //        }
    //    }

    //    timer += Time.deltaTime;

    //}
}
