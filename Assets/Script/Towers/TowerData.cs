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
    public float price;
    public float baseDamage;
    public float normalBase;
    public float armorBase;
    public float magicArmorBase;
    public Sprite icon;
    public GameObject startEffect;
    public GameObject bullet;
    public GameObject endEffect;
}
