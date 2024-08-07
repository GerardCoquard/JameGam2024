using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TowerMinas : Tower
{
    public int numberMinas;
    public LayerMask myLayerMask;
    List<RaycastHit> hits;
    // Start is called before the first frame update
    public override void StartTower(TowerData data)
    {
        base.StartTower(data);
        GetSplinesInRange();
    }
    public void GetSplinesInRange()
    {
        hits = new List<RaycastHit>(Physics.SphereCastAll(transform.position, range, transform.forward, 1f, myLayerMask));
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (fireRateTimer < 1 / fireRate)
        {
            fireRateTimer += Time.deltaTime;
        }
            
        else
        {
            fireRateTimer = 0;
            if(hits.Count > 0)
            {
                int randomPath = Random.Range(0, hits.Count);
                GameObject bullet = GameObject.Instantiate(bulletPrefab, hits[randomPath].transform.position, Quaternion.identity);
            }
            
        }
    }
}
