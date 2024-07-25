using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAOE : Tower
{
    float timer;

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
            float distance = Vector3.Distance(enemy.position, transform.position);
            if (distance <= range && timer >= 1/fireRate)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, enemy.position, Quaternion.identity);
                timer = 0;
                animator.SetTrigger("aoe");
                StartCoroutine(FollowEnemy(bullet.transform, enemy.transform));
                StartCoroutine(SetDmgToAbilitie(bullet));
                Destroy(bullet, 3.1f);
            }
        }
        timer += Time.deltaTime;
    }
    IEnumerator SetDmgToAbilitie(GameObject bullet)
    {
        yield return new WaitForSeconds(1.2f);
        bullet.GetComponent<ActivateAreaDamage>().SetVariables(baseDamage, this);
    }
    IEnumerator FollowEnemy(Transform bullet,Transform enemy)
    {
        float time = 0;
        MoveEnemie enemyMove = enemy.GetComponent<MoveEnemie>();
        while (time < 3)
        {
            if(enemy.gameObject == null)
            {
                yield return null;
            }
            time += Time.deltaTime;
            bullet.position = enemyMove.publicPos;
            yield return new WaitForEndOfFrame();
        }
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
    
    

    // Update is called once per frame
    void Update()
    {
        Action();
    }
}
