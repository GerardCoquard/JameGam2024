using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShpereDamage : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.GetComponent<Enemy>().DamageEnemy(damage);
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
