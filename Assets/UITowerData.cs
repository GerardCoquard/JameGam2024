using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITowerData : MonoBehaviour
{
    private Tower _tower;

    private void Start()
    {
        InputManager.OnGridClick += CheckIfTowerSelected;
        gameObject.SetActive(false);
    }

    private void CheckIfTowerSelected(Vector3 pos, string layer)
    {
        if (layer != "Wizards")
            gameObject.SetActive(false);
        else
            SetSelectedTower(GetTowerOnCursor(pos));
            
    }
    
    private void SetSelectedTower(Tower tower)
    {
        _tower = tower;
        //Set all UI
    }

    private Tower GetTowerOnCursor(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(pos));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, LayerMask.NameToLayer("Wizards")))
        {
            return hit.collider.GetComponentInChildren<Tower>();
        }
        return null;
    }
}
