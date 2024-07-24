using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCloseDistance : Tower
{
    [SerializeField] Transform enemy;
    [SerializeField] Transform instantiatePoint;
    [SerializeField] AnimationCurve spawnCurve;
    float timer;

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
            float distance = Vector3.Distance(enemy.position, transform.position);
            if (distance <= baseRange && timer >= baseFireRate)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, instantiatePoint.position, Quaternion.Euler(-90,0,0));
                float dañoRestante = baseDamage;
                for (int i = 0; i < 3; i++)
                {
                    float armorDmg = CalculateDamageArmor(dañoRestante);
                    float magicArmorDmg = CalculateDamageMagicArmor(dañoRestante);
                    float normalDmg = CalculateDamageMagicArmor(dañoRestante);
                    enemy.GetComponent<Enemy>().DamageEnemy(normalDmg, out dañoRestante, armorDmg, magicArmorDmg);
                    if (dañoRestante <= 0)
                    {
                        break;
                    }
                }
                bullet.GetComponent<CapsuleCollider>().radius = baseRange;
                var shape = bullet.GetComponent<ParticleSystem>().shape;
                timer = 0;
                bullet.transform.parent = instantiatePoint;
                bullet.transform.localPosition = Vector3.zero;
                Destroy(bullet.gameObject, 2f);
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
    
    

    // Update is called once per frame
    void Update()
    {
        Action();
    }
}
