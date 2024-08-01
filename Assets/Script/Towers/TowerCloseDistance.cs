using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCloseDistance : Tower
{
    void InstantiateBullet()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.Euler(-90, 0, 0));
        audioSourceAction.PlayOneShot(audioSourceAction.clip);
        bullet.GetComponent<ShpereDamage>().SetVariables(damage, this);
        bullet.GetComponent<CapsuleCollider>().radius = range;
        var shape = bullet.GetComponent<ParticleSystem>().shape;
        shape.radius = range;
        
        bullet.transform.parent = bulletInstantiatePoint;
        bullet.transform.localPosition = Vector3.zero;
        Destroy(bullet.gameObject, 2f);
    }
}
