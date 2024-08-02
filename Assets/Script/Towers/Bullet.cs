using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Tower tower;
    public Transform enemy;
    protected Vector3 enemyPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy!= null)
        {
            enemyPos = enemy.position;
        }
    }
    public virtual void Action(Tower _tower, Transform _enemy)
    {
        tower = _tower;
        enemy = _enemy;
    }
}
