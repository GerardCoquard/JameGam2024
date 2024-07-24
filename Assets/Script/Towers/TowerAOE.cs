using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAOE : Tower
{
    [SerializeField] Transform enemy;
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
            if (distance <= baseRange && timer >= baseFireRate)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, enemy.position, Quaternion.identity);
                //bullet.GetComponent<ActivateAreaDamage>().damage = damage;
                timer = 0;
                StartCoroutine(FollowEnemy(bullet.transform, enemy.transform));
                Destroy(bullet, 3);
            }
        }
        timer += Time.deltaTime;
    }
    IEnumerator FollowEnemy(Transform bullet,Transform enemy)
    {
        float time = 0;
        MoveEnemie enemyMove = enemy.GetComponent<MoveEnemie>();
        while (time < 3)
        {
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
