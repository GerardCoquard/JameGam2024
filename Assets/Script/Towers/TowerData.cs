using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BasicData", menuName = "Tower/Basic/BasicData")]
public class TowerData : ScriptableObject
{
    public GameObject prefab;
    public string towerName;
    public float fireRate;
    public float range;
    public int damage;
    public int price;
    public int armorPenetration;
    public int magicArmorPenetration;
    public Sprite icon;
    public GameObject startEffect;
    public GameObject bullet;
    public GameObject endEffect;
}
