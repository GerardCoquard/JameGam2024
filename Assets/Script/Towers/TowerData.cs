using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BasicData", menuName = "Tower/Basic/BasicData")]
public class TowerData : ScriptableObject
{
    public GameObject prefab;
    public string towerName;
    public Sprite icon;
    public GameObject startEffect;
    public GameObject bullet;
    public GameObject endEffect;
    public int price;
    public float range;
    public float fireRate;
    public int baseDamage;
    public float normalBase;
    public float armorBase;
    public float magicArmorBase;
}
