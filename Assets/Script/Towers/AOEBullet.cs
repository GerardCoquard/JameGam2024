using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBullet : Bullet
{
    Vector3 enemyPos;
    public float activateCollider = 1.2f;
    public float disableCollider = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
        StartCoroutine(SetDmgToAbilitie());
    }
    IEnumerator SetDmgToAbilitie()
    {
        yield return new WaitForSeconds(activateCollider);
        GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(disableCollider);
        GetComponent<BoxCollider>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(enemy != null)
        {
            enemyPos = enemy.position;
        }
        transform.position = enemyPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.GetComponent<Enemy>().DamageEnemy(tower, tower.damage);
    }
}
