using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicData", menuName = "Tower/Basic/BasicData")]
public class TowerData : ScriptableObject
{
    public float fireRate;
    public float range;
    public GameObject startEffect;
    public GameObject bullet;
    public GameObject endEffect;
}
