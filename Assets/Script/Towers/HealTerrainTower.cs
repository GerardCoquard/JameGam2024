using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTerrainTower : Tower
{
    [SerializeField] GameObject sphereHealPrefab;
    [SerializeField] GameObject planeHealPrefab;
    Transform groundPoint;

    private void Awake()
    {            
    }
    public override void StartTower()
    {
        base.StartTower();
        groundPoint = transform.GetChild(1);
        StartCoroutine(CreateHealTerrain());
    }

    public override void Action()
    {
        base.Action();       
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
    IEnumerator CreateHealTerrain()
    {
        yield return new WaitForSeconds(3);
        GameObject sphere = GameObject.Instantiate(sphereHealPrefab, groundPoint.position, Quaternion.identity);
        GameObject plane = GameObject.Instantiate(planeHealPrefab, groundPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        Destroy(sphere.gameObject);
        Destroy(plane.gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {
        Action();
    }
}
