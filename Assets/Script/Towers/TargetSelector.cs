using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    private List<Enemy> _enemyList;
    public TargetBehaviour behaviour = TargetBehaviour.First;

    public void Initialize()
    {
        _enemyList = new List<Enemy>();
        behaviour = TargetBehaviour.First;
    }
    
    public Transform GetTarget()
    {
        CleanList();
        
        if (_enemyList.Count <= 0)
            return null;

        switch (behaviour)
        {
            case TargetBehaviour.First:
                return GetFirstEnemy();
            
            case TargetBehaviour.Last:
                return GetLastEnemy();
            
            case TargetBehaviour.Health:
                return GetMostHealthEnemy();
            
            case TargetBehaviour.Armor:
                return GetMostArmorEnemy();
            
            case TargetBehaviour.Magic:
                return GetMostMagicEnemy();
            
            default:
                return null;
        }
    }

    private void CleanList()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i] == null)
            {
                _enemyList.RemoveAt(i);
                i--;
            }
        }
    }

    private Transform GetFirstEnemy()
    {
        return _enemyList.OrderByDescending(x => x.GetPathDistance()).ToList()[0].transform;
    }
    
    private Transform GetLastEnemy()
    {
        return _enemyList.OrderBy(x => x.GetPathDistance()).ToList()[0].transform;
    }
    
    private Transform GetMostHealthEnemy()
    {
        int indx = 0;
        bool enemyFound = false;
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i].GetHealth() <= 0)
                continue;
            
            if (_enemyList[i].GetArmor() <= 0 && _enemyList[i].GetMagic() <= 0)
                if (_enemyList[i].GetHealth() >= _enemyList[indx].GetHealth())
                {
                    enemyFound = true;
                    indx = i;
                }
        }

        if (enemyFound)
            return _enemyList[indx].transform;
        else
            return GetFirstEnemy();
    }
    
    private Transform GetMostArmorEnemy()
    {
        int indx = 0;
        bool enemyFound = false;
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i].GetArmor() <= 0)
                continue;
            
            if (_enemyList[i].GetMagic() <= 0)
                if (_enemyList[i].GetArmor() >= _enemyList[indx].GetArmor())
                {
                    enemyFound = true;
                    indx = i;
                }
        }

        if (enemyFound)
            return _enemyList[indx].transform;
        else
            return GetFirstEnemy();
    }
    
    private Transform GetMostMagicEnemy()
    {
        int indx = 0;
        bool enemyFound = false;
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i].GetMagic() <= 0)
                continue;
            
            if (_enemyList[i].GetMagic() >= _enemyList[indx].GetMagic())
            {
                enemyFound = true;
                indx = i;
            }
        }

        if (enemyFound)
            return _enemyList[indx].transform;
        else
            return GetFirstEnemy();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _enemyList.Add(other.GetComponentInParent<Enemy>());
    }
    
    private void OnTriggerExit(Collider other)
    {
        _enemyList.Remove(other.GetComponentInParent<Enemy>());
    }

    public void ChangeBehaviour()
    {
        if (behaviour == TargetBehaviour.Magic)
            behaviour = TargetBehaviour.First;
        else
            behaviour++;
    }

    public string GetBehaviour()
    {
        switch (behaviour)
        {
            case TargetBehaviour.First:
                return "First";
            
            case TargetBehaviour.Last:
                return "Last";
            
            default:
                return "More ";
        }
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
