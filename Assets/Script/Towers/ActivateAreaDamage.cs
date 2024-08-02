using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAreaDamage : Bullet
{
    [SerializeField] LayerMask layerMask;
    Vector3 initialScale;
    Transform laser;
    BoxCollider boxCollider;
    [SerializeField] AnimationCurve laserCurve;
    private void Awake()
    {

    }
    private void Start()
    {
        laser = transform.GetChild(0).GetChild(0);
        boxCollider = laser.GetComponent<BoxCollider>();
        initialScale = new Vector3(1, 1, tower.range);
        transform.localEulerAngles = new Vector3(-80, 180, 0);
        StartCoroutine(MoveBullet());
    }
    IEnumerator MoveBullet()
    {
        transform.localScale = new Vector3(1, 1, 1);
        Debug.Log(tower.bulletInstantiatePoint);
        transform.parent = tower.bulletInstantiatePoint;
        Vector3 startRot = new Vector3(-80, 180, 0);
        Vector3 endRot = new Vector3(20, 180, 0);
        float time = 0;
        while (time < 1.5f)
        {
            time += Time.deltaTime;
            float percentageDuration = time / 1.5f;
            transform.localEulerAngles = Vector3.Lerp(startRot, endRot, laserCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
        tower.animator.SetTrigger("Laser End");
        transform.GetComponent<Animator>().SetTrigger("despawn");
        yield return new WaitForSeconds(1);
        Destroy(transform.gameObject);
    }
    public override void Action(Tower _tower, Transform _enemy)
    {
        base.Action(_tower, _enemy);
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(laser.parent.position, -laser.parent.forward, out hit, 1000f, layerMask))
        {           
            if (Vector3.Distance(transform.position, hit.point) > tower.range*2)
            {
                laser.parent.localScale = initialScale;
            }
            else
            {
                laser.parent.localScale = new Vector3(laser.parent.localScale.x, laser.parent.localScale.y, Vector3.Distance(laser.parent.position, hit.point));
            }
        }
        else
        {
            laser.parent.localScale = initialScale;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("damage");
        other.transform.parent.GetComponent<Enemy>().DamageEnemy(tower,tower.damage);
    }
}
