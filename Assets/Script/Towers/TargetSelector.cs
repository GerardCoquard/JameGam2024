using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    protected List<Enemy> enemyList;
    public TargetBehaviour behaviour;

    public void Initialize()
    {
        enemyList = new List<Enemy>();
    }
    
    public Transform GetTarget()
    {
        CleanList();
        
        if (enemyList.Count <= 0)
            return null;

        switch (behaviour)
        {
            case TargetBehaviour.First:
                break;
            
            case TargetBehaviour.Last:
                break;
            
            case TargetBehaviour.Health:
                break;
            
            case TargetBehaviour.Armor:
                break;
            
            case TargetBehaviour.Magic:
                break;
            
            default:
                return null;
                break;
        }
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

    private Transform GetFirstEnemy()
    {
        return enemyList.OrderBy(x => x.GetPathDistance()).ToList()[0].transform;
    }
    
    private Transform GetLastEnemy()
    {
        return enemyList.OrderByDescending(x => x.GetPathDistance()).ToList()[0].transform;
    }
    
    private Transform GetMostHealthEnemy()
    {
        enemyList.OrderBy(x => x.GetHealth());
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].GetHealth() <= 0)
                continue;
            
            if (enemyList[i].GetArmor() <= 0 && enemyList[i].GetMagic() <= 0)
                return enemyList[i].transform;
        }

        if (enemyList[0].GetHealth() > 0)
            return enemyList[0].transform;
        else
            return GetFirstEnemy();
    }
    
    private Transform GetMostArmorEnemy()
    {
        enemyList.OrderBy(x => x.GetArmor());
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].GetArmor() <= 0)
                continue;
            
            if (enemyList[i].GetMagic() <= 0)
                return enemyList[i].transform;
        }

        if (enemyList[0].GetArmor() > 0)
            return enemyList[0].transform;
        else
            return GetFirstEnemy();
    }
    
    private Transform GetMostMagicEnemy()
    {
        enemyList.OrderBy(x => x.GetMagic());
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].GetMagic() <= 0)
                continue;
            
            return enemyList[i].transform;
        }

        if (enemyList[0].GetMagic() > 0)
            return enemyList[0].transform;
        else
            return GetFirstEnemy();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        enemyList.Add(other.GetComponentInParent<Enemy>());
    }
    
    private void OnTriggerExit(Collider other)
    {
        enemyList.Remove(other.GetComponentInParent<Enemy>());
    }
}

public enum TargetBehaviour
{
    First,
    Last,
    Health,
    Armor,
    Magic
}
