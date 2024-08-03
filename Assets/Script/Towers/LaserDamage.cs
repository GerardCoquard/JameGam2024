using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ActivateAreaDamage info = transform.parent.parent.GetComponent<ActivateAreaDamage>();
        other.transform.parent.GetComponent<Enemy>().DamageEnemy(info.tower, info.tower.damage);
    }
}
