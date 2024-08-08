using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TowerMinas : Tower
{
    public LayerMask myLayerMask;
    List<RaycastHit> hits;
    List<Vector3> bulletPos;
    [SerializeField] float heightPath;
    int indexBullet = 0;
    // Start is called before the first frame update
    public override void StartTower(TowerData data)
    {
        bulletPos = new List<Vector3>();
        base.StartTower(data);
        GetSplinesInRange();
    }
    public void RemovePos(Vector3 pos)
    {
        pos.y -= heightPath;
        bulletPos.Remove(pos);
        indexBullet = 0;
    }
    public void GetSplinesInRange()
    {
        indexBullet = 0;
        hits = new List<RaycastHit>(Physics.SphereCastAll(transform.position, range, transform.forward, 1f, myLayerMask));

        for (int i = 0; i < hits.Count; i++)
        {
            if (Vector3.Distance(hits[i].transform.position, transform.position) > range)
            {
                hits.RemoveAt(i);
                i--;
            }
        }
    }
    public override void AddLevelRange()
    {
        base.AddLevelRange();
        GetSplinesInRange();
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
            if(hits.Count > bulletPos.Count)
            {
                Vector3 pos = hits[indexBullet].transform.position;
                indexBullet++;
                if (bulletPos.Contains(pos))
                {
                    bool hasFoundPos = false;
                    while (!hasFoundPos)
                    {
                        pos = hits[indexBullet].transform.position;
                        if (!bulletPos.Contains(pos))
                        {
                            hasFoundPos = true;
                        }
                        indexBullet++;
                    }
                }
                bulletPos.Add(pos);
                pos.y += heightPath;
                GameObject bullet = GameObject.Instantiate(bulletPrefab, pos, Quaternion.identity);
                bullet.GetComponent<Bullet>().Action(this, target);
            }
            
        }
    }
}
