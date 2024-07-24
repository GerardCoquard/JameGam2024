using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITowerData : MonoBehaviour
{
    private Tower _tower;
    [SerializeField] private LayerMask _layer;

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
        //Not working?
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(pos));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
        {
            return hit.collider.GetComponentInChildren<Tower>();
        }
        return null;
    }

    private void DeselectTower()
    {
        gameObject.SetActive(false);
        if(_tower!=null)
            _tower.OnStatChanged -= SetVisuals;
        RangeManager.instance.Hide();
    }
}
