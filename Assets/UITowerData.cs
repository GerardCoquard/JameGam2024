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
            DeselectTower();
        else
            SetSelectedTower(GetTowerOnCursor(pos));

    }
    
    private void SetSelectedTower(Tower tower)
    {
        _tower = tower;
        gameObject.SetActive(true);
        SetVisuals();
        _tower.OnStatChanged += SetVisuals;
    }

    private void SetVisuals()
    {
        RangeManager.instance.Show(_tower.transform.position,_tower.range);
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

    private void DeselectTower()
    {
        gameObject.SetActive(false);
        _tower.OnStatChanged -= SetVisuals;
        RangeManager.instance.Hide();
    }
}
