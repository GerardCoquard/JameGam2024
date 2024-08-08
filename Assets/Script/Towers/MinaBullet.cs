using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinaBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.GetComponent<Enemy>().DamageEnemy(tower, tower.damage);
        tower.GetComponent<TowerMinas>().RemovePos(transform.position);
        Destroy(gameObject);
    }
}
