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
            if (distance <= range && timer >= fireRate)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, instantiatePoint.position, Quaternion.Euler(-90,0,0));
                float daņoRestante = baseDamage;
                for (int i = 0; i < 3; i++)
                {
                    int armorDmg = Mathf.RoundToInt(CalculateDamageArmor(daņoRestante));
                    int magicArmorDmg = Mathf.RoundToInt(CalculateDamageMagicArmor(daņoRestante));
                    int normalDmg = Mathf.RoundToInt(CalculateDamageNormal(daņoRestante));
                    enemy.GetComponent<Enemy>().DamageEnemy(normalDmg, out daņoRestante, armorDmg, magicArmorDmg);
                    if (daņoRestante <= 0)
                    {
                        break;
                    }
                    if(i == 0)
                    {
                        daņoRestante = Mathf.RoundToInt(MagicArmorToBase(daņoRestante));
                    }
                    else if(i == 1)
                    {
                        daņoRestante = Mathf.RoundToInt(ArmorToBase(daņoRestante));
                    }
                }
                bullet.GetComponent<CapsuleCollider>().radius = range;
                var shape = bullet.GetComponent<ParticleSystem>().shape;
                shape.radius = range;
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
