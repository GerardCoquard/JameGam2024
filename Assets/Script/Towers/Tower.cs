using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [HideInInspector]
    public int health;
    protected int maxHealth;
    protected float actionRate;

    [HideInInspector]
    bool isAlive = true;
    public virtual void StartTower()
    {
        
    }
    public virtual void Action()
    {

    }
    public virtual void EndTower()
    {

    }
}
