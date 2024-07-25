using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShpereDamage : MonoBehaviour
{
    float baseDamage;
    Tower tower;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.GetComponent<Enemy>().DamageEnemy(tower, baseDamage);
    }
    public void SetVariables(float _baseDamage, Tower _tower)
    {
        baseDamage = _baseDamage;
        tower = _tower;
        if (GetComponent<CapsuleCollider>() != null)
        {
            GetComponent<CapsuleCollider>().enabled = true;
        }
        GetComponent<CapsuleCollider>().radius = _tower.range;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
