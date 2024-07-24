using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : MonoBehaviour
{
    public float slowMultiplier;
    private Vector3 anteriorScale;
    [SerializeField] Material purifiedMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale != anteriorScale)
        {
            anteriorScale = transform.localScale;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.localScale.x/2);
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    hitCollider.gameObject.layer = LayerMask.NameToLayer("PurifiedGround");
                    hitCollider.GetComponent<MeshRenderer>().material = purifiedMat;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.GetComponent<MoveEnemie>().speed /= slowMultiplier;
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent.GetComponent<MoveEnemie>().speed *= slowMultiplier;
    }
}
