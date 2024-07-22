using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAreaDamage : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActivateCollider", 1.2f);
    }
    void ActivateCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("a");
        other.transform.parent.GetComponent<Enemy>().DamageEnemy(damage);
    }
}
