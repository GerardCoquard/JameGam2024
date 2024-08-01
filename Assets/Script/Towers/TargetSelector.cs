using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    protected List<Transform> enemyList;

    public void Initialize()
    {
        enemyList = new List<Transform>();
    }
    
    public Transform GetTarget()
    {
        CleanList();
        
        if (enemyList.Count != 0)
        {
            return enemyList[0];
        }

        return null;
    }

    private void CleanList()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
                i--;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        enemyList.Add(other.transform.parent);
    }
    
    private void OnTriggerExit(Collider other)
    {
        enemyList.Remove(other.transform.parent);
    }
}
