using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAreaDamage : MonoBehaviour
{
    float baseDamage;
    Tower tower;
    [SerializeField] float invokeColliderTime;
    [SerializeField] bool adaptLaser;
    [SerializeField] LayerMask layerMask;
    Vector3 initialScale;
    private void Awake()
    {
        initialScale = new Vector3(1,1,70);
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActivateCollider", invokeColliderTime);
    }
    void ActivateCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    public void SetVariables(float _baseDamage, Tower _tower)
    {
        baseDamage = _baseDamage;
        tower = _tower;
    }
    // Update is called once per frame
    void Update()
    {
        if (adaptLaser)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.parent.position, -transform.parent.forward, out hit, 1000f, layerMask))
            {
                Debug.Log("a");
                transform.parent.localScale = new Vector3(transform.parent.localScale.x, transform.parent.localScale.y, Vector3.Distance(transform.parent.position, hit.point));
            }
            else
            {
                transform.parent.localScale = initialScale;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {      
        other.transform.parent.GetComponent<Enemy>().DamageEnemy(tower,baseDamage);
    }
}
