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
            float scale = transform.localScale.x/ 2;


            Collider[] hitColliders = Physics.OverlapSphere(transform.position, scale);
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

    private void OnTriggerStay(Collider other)
    {
        if (!other.transform.parent.GetComponent<MoveEnemy>().slowed)
        {
            other.transform.parent.GetComponent<MoveEnemy>().speed /= slowMultiplier;
            other.transform.parent.GetComponent<MoveEnemy>().slowed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.GetComponent<MoveEnemy>().slowed)
        {
            other.transform.parent.GetComponent<MoveEnemy>().speed *= slowMultiplier;
            other.transform.parent.GetComponent<MoveEnemy>().slowed = false;
        }
    }
}
