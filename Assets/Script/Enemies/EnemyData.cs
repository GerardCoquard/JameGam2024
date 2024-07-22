using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyData")]

public class EnemyData : ScriptableObject
{
    public float health;
    public int damage;
}
