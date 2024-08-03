using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAreaDamage : Bullet
{
    [SerializeField] LayerMask layerMask;
    Transform laser;
    BoxCollider boxCollider;
    [SerializeField] AnimationCurve laserCurve;
    [SerializeField] Transform bulletEnd;
    public float timeAnim = 1.5f;
    private void Awake()
    {

    }
    private void Start()
    {
        laser = transform.GetChild(0).GetChild(0);
        boxCollider = laser.GetComponent<BoxCollider>();
        transform.localEulerAngles = new Vector3(-80, 180, 0);
        StartCoroutine(MoveBullet());
    }
    IEnumerator MoveBullet()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.parent = tower.bulletInstantiatePoint;
        Vector3 startRot = new Vector3(-80, 180, 0);
        Vector3 endRot = new Vector3(20, 180, 0);
        float time = 0;
        while (time < timeAnim)
        {
            time += Time.deltaTime;
            float percentageDuration = time / timeAnim;
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
            if (Vector3.Distance(transform.position, hit.point) > tower.range)
            {
                laser.parent.localScale = new Vector3(1, 1, tower.range * 2);
                bulletEnd.localPosition = new Vector3(0, 0, -tower.range*4);
            }
            else
            {
                laser.parent.localScale = new Vector3(laser.parent.localScale.x, laser.parent.localScale.y, Vector3.Distance(laser.parent.position, hit.point)*2);
                bulletEnd.position = hit.point;
            }
        }
        else
        {
            bulletEnd.localPosition = new Vector3(0, 0, -tower.range*4);
            laser.parent.localScale = new Vector3(1, 1, tower.range * 2);
        }
    }
}
