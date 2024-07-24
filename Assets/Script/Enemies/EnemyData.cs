using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyData")]

public class EnemyData : ScriptableObject
{
    public int health;
    public float damage;
    public float armor;
    public float magicArmor;
}
