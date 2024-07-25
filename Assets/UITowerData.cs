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
        int price = _upgradesManager.CalculatePrice(GameManager.gameData.upgradeBasePriceNormal, _tower.normalLevel);
        if (GameManager.HaveCurrency(price))
        {
            _tower.AddLevelNormal();
            GameManager.RemoveCurrency(price);
        }
        else
            UISpawner.instance.SpawnTextWithColorFromUIPos(_upgradesManager._healthUpgrade.transform.position, "Not enough currency", Color.red);
    }
    
    public void UpgradeArmor()
    {
        int price = _upgradesManager.CalculatePrice(GameManager.gameData.upgradeBasePriceArmor, _tower.armorLevel);
        if (GameManager.HaveCurrency(price))
        {
            _tower.AddLevelArmor();
            GameManager.RemoveCurrency(price);
        }
        else
            UISpawner.instance.SpawnTextWithColorFromUIPos(_upgradesManager._armorUpgrade.transform.position, "Not enough currency", Color.red);
    }
    
    public void UpgradeMagic()
    {
        int price = _upgradesManager.CalculatePrice(GameManager.gameData.upgradeBasePriceMagicArmor, _tower.magicArmorLevel);
        if (GameManager.HaveCurrency(price))
        {
            _tower.AddLevelmagicArmor();
            GameManager.RemoveCurrency(price);
        }
        else
            UISpawner.instance.SpawnTextWithColorFromUIPos(_upgradesManager._magicUpgrade.transform.position, "Not enough currency", Color.red);
    }
    
    public void UpgradeFireRate()
    {
        int price = _upgradesManager.CalculatePrice(GameManager.gameData.upgradeBasePriceFireRate, _tower.fireRateLevel);
        if (GameManager.HaveCurrency(price))
        {
            _tower.AddLevelFireRate();
            GameManager.RemoveCurrency(price);
        }
        else
            UISpawner.instance.SpawnTextWithColorFromUIPos(_upgradesManager._fireRateUpgrade.transform.position, "Not enough currency", Color.red);
    }
    
    public void UpgradeRange()
    {
        int price = _upgradesManager.CalculatePrice(GameManager.gameData.upgradeBasePriceRange, _tower.rangeLevel);
        if (GameManager.HaveCurrency(price))
        {
            _tower.AddLevelRange();
            GameManager.RemoveCurrency(price);
        }
        else
            UISpawner.instance.SpawnTextWithColorFromUIPos(_upgradesManager._rangeUpgrade.transform.position, "Not enough currency", Color.red);
    }
}
