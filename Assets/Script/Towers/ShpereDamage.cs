using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShpereDamage : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.GetComponent<Enemy>().DamageEnemy(tower, tower.damage);
    }
    public override void Action(Tower _tower, Transform _enemy)
    {
        base.Action(_tower, _enemy);
        if (GetComponent<CapsuleCollider>() != null)
        {
            GetComponent<CapsuleCollider>().enabled = true;
        }
        GetComponent<CapsuleCollider>().radius = _tower.range;
        var shape = GetComponent<ParticleSystem>().shape;
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        shape.radius = _tower.range;
        Destroy(gameObject, 2f);
    }
}
