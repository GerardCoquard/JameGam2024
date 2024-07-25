using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyData")]

public class EnemyData : ScriptableObject
{
    public int health;
    public int armor;
    public int magicArmor;
    public float speed;
    public int damage;
    public int currency;
    public GameObject EnemyPrefab;
}
