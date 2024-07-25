using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTerrainTower : Tower
{
    [SerializeField] GameObject sphereHealPrefab;
    [SerializeField] AnimationCurve animationCurve;
    GameObject sphere;
    private void Awake()
    {            
    }
    public override void StartTower()
    {
        base.StartTower();
        StartCoroutine(SpawnSphere());
    }
    IEnumerator SpawnSphere()
    {
        yield return new WaitForSeconds(0.2f);
        audioSourceAction.PlayOneShot(audioSourceAction.clip);
        sphere = GameObject.Instantiate(sphereHealPrefab, transform.position, Quaternion.identity);
        Vector3 endScale = new Vector3(range*2, range*2, range*2);
        float time = 0;
        while (time < 1.5f)
        {
            time += Time.deltaTime;
            float percentageDuration = time / 1.5f;
            sphere.transform.localScale = Vector3.Lerp(Vector3.zero, endScale, animationCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator MakeSphereBigger()
    {
        Vector3 startScale = new Vector3(anteriorRange*2, anteriorRange*2, anteriorRange*2);
        Vector3 endScale = new Vector3(range*2, range*2, range*2);
        float time = 0;
        while (time < 1.5f)
        {
            time += Time.deltaTime;
            float percentageDuration = time / 1.5f;
            sphere.transform.localScale = Vector3.Lerp(startScale, endScale, animationCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
    }
    public override void Action()
    {
        if (hasChangedRange)
        {
            hasChangedRange = false;
            StopAllCoroutines();
            StartCoroutine(MakeSphereBigger());
        } 
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
