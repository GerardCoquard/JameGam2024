using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : Bullet
{
    [SerializeField] AnimationCurve bulletSpeedCurve;
    [SerializeField] float bulletSpeedMultiplier;
    public BulletData bulletData;
    float distance;
    int damage;
    private ParticleSystem particleSystem;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(enemy);
        damage = bulletData.damage;
        particleSystem = GetComponent<ParticleSystem>();
        distance = Vector3.Distance(transform.position, enemy.position);
        StartCoroutine(MoveBullet());
    }

    IEnumerator MoveBullet()
    {
        Vector3 startPos = transform.position;
        float time = 0;
        float targetTime = distance / bulletSpeedMultiplier;
        while (time < targetTime)
        {
            time += Time.deltaTime;
            float percentageDuration = time / targetTime;
            transform.position = Vector3.Lerp(startPos, enemy.position, bulletSpeedCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
