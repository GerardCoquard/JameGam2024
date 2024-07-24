using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyData")]

public class EnemyData : ScriptableObject
{
    public float damage;
    public int health;
    public float armor;
    public float magicArmor;
    public float speed;
    public int currency;
}
