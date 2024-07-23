using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCloseDistance : Tower
{
    [SerializeField] TowerData data;
    float fireRate;
    int damage;
    float range;
    GameObject startEffect;
    GameObject bulletPrefab;
    GameObject endEffect;
    [SerializeField] Transform enemy;
    [SerializeField] Transform instantiatePoint;
    [SerializeField] AnimationCurve spawnCurve;
    float timer;

    private void Awake()
    {      
        fireRate = data.fireRate;
        range = data.range;
        startEffect = data.startEffect;
        bulletPrefab = data.bullet;
        endEffect = data.endEffect;
        damage = data.damage;
        health = maxHealth;

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
            float distance = Vector3.Distance(enemy.position, transform.position);
            if (distance <= range && timer >= fireRate)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, instantiatePoint.position, Quaternion.Euler(-90,0,0));
                bullet.GetComponent<ShpereDamage>().damage = damage;
                bullet.GetComponent<CapsuleCollider>().radius = range;
                timer = 0;
                bullet.transform.parent = instantiatePoint;
                bullet.transform.localPosition = Vector3.zero;
                StartCoroutine(CreateSphere(bullet.transform));
            }
        }
        timer += Time.deltaTime;
    }
    IEnumerator CreateSphere(Transform bullet)
    {
        //float time = 0;
        //Vector3 endScale = new Vector3(range, range, range);
        //ParticleSystem ps = bullet.GetComponent<ParticleSystem>();
        //SphereCollider sc = bullet.GetComponent<SphereCollider>();
        //ps.shape.radius = 1;
        //while (time < 2f)
        //{
        //    time += Time.deltaTime;
        //    float percentageDuration = time / 2f;
        //    ps.shape.radius = Mathf.Lerp(0, range, spawnCurve.Evaluate(percentageDuration));
        //    sc.radius = 
        //    yield return new WaitForEndOfFrame();
        //}
        var shape = bullet.GetComponent<ParticleSystem>().shape;
        shape.radius = range;
        yield return new WaitForSeconds(2);
        Destroy(bullet.gameObject);
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
    
    

    // Update is called once per frame
    void Update()
    {
        Action();
        if(health<= 0)
        {
            EndTower();
        }
    }
}
