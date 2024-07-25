using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITowerData : MonoBehaviour
{
    private Tower _tower;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private UpgradesManager _upgradesManager;
    [SerializeField] private TowerInfo _towerInfo;

    private void Start()
    {
        InputManager.OnGridClick += CheckIfTowerSelected;
        gameObject.SetActive(false);
    }

    private void CheckIfTowerSelected(Vector3 pos, string layer, bool plane)
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
        _upgradesManager.SetData(_tower);
        _towerInfo.SetData(_tower);
    }

    private Tower GetTowerOnCursor(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(pos));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer,QueryTriggerInteraction.Ignore))
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

    public void SellTower()
    {
        GameManager.AddCurrency(_tower.data.price);
        DeselectTower();
        Destroy(_tower.gameObject);
    }
    
    public void UpgradeHealth()
    {
        _tower.AddLevelNormal();
    }
    
    public void UpgradeArmor()
    {
        _tower.AddLevelArmor();
    }
    
    public void UpgradeMagic()
    {
        _tower.AddLevelmagicArmor();
    }
    
    public void UpgradeFireRate()
    {
        _tower.AddLevelFireRate();
    }
    
    public void UpgradeRange()
    {
        _tower.AddLevelRange();
    }
}
