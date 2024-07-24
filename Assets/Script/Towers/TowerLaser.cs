using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLaser : Tower
{
    [SerializeField] Transform enemy;
    float timer;
    [SerializeField]Transform bulletInstantiatePoint;
    [SerializeField] AnimationCurve laserCurve;
    bool freezeRotation = false;

    private ParticleSystem particleSystem;

    private void Awake()
    {      
        timer = 0;
    }
    public override void StartTower()
    {
        base.StartTower();
    }
    public override void Action()
    {
        base.Action();
        if(enemy != null)
        {
            Vector3 targetDirection = enemy.position - transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = targetRotation;
            float distance = Vector3.Distance(enemy.position, bulletInstantiatePoint.position);
            if (distance <= baseRange && timer >= baseFireRate)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletInstantiatePoint.position, Quaternion.identity);
                bullet.transform.GetChild(0).GetChild(0).GetComponent<ActivateAreaDamage>().SetVariables(baseDamage, this);
                bullet.transform.parent = bulletInstantiatePoint;
                timer = 0;
                StartCoroutine(MoveBullet(distance, enemy, bullet.transform));
            }
        }
        
        timer += Time.deltaTime;

    }
    
    public override void EndTower()
    {
        base.EndTower();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartTower();
    }
    IEnumerator MoveBullet(float distance,Transform enemy, Transform bullet)
    {

        //bullet.GetChild(0).GetChild(0).GetComponent<ActivateAreaDamage>().damage = baseDamage;
        Vector3 startRot = new Vector3(-80,180,0);
        Vector3 endRot = new Vector3(20,180,0);
        float time = 0;
        while (time < 1.5f)
        {
            time += Time.deltaTime;
            float percentageDuration = time / 1.5f;
            bullet.localEulerAngles = Vector3.Lerp(startRot, endRot, laserCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
        freezeRotation = false;
        bullet.GetComponent<Animator>().SetTrigger("despawn");
        yield return new WaitForSeconds(1);
        Destroy(bullet.gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {
        Action();
    }
}
