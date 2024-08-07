using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UITowerData : MonoBehaviour
{
    private Tower _tower;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private UpgradesManager _upgradesManager;
    [SerializeField] private TowerInfo _towerInfo;
    [SerializeField] private TextMeshProUGUI _behaviourText;
    [SerializeField] private Image _behaviourIconText;
    [SerializeField] private float offset;
    [SerializeField] private Color colorHealth;
    [SerializeField] private Color colorArmor;
    [SerializeField] private Color colorMagic;
    [SerializeField] private GameObject targetGameObject;
    private Vector3 initPos;

    private void Start()
    {
        InputManager.OnGridClick += CheckIfTowerSelected;
        gameObject.SetActive(false);
        initPos = _behaviourText.transform.localPosition;
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
        //Show current target
        _upgradesManager.SetData(_tower);
        _towerInfo.SetData(_tower);
        UpdateBehaviour();
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
        GameManager.AddCurrency(_tower.basePrice);
        DeselectTower();
        Destroy(_tower.gameObject);
    }
    
    public void UpgradeHealth()
    {
        int price = _upgradesManager.CalculatePrice(GameManager.gameData.upgradeBasePriceNormal, _upgradesManager.GetLevelsTotal(_tower));
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
        int price = _upgradesManager.CalculatePrice(GameManager.gameData.upgradeBasePriceArmor, _upgradesManager.GetLevelsTotal(_tower));
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
        int price = _upgradesManager.CalculatePrice(GameManager.gameData.upgradeBasePriceMagicArmor, _upgradesManager.GetLevelsTotal(_tower));
        if (GameManager.HaveCurrency(price))
        {
            _tower.AddLevelMagicArmor();
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

    public void ChangeBehaviour()
    {
        _tower.GetTargetSelector().ChangeBehaviour();
        UpdateBehaviour();
    }

    private void UpdateBehaviour()
    {
        targetGameObject.SetActive(_tower.towerName != "Purifier");
        
        bool normalTarget = _tower.GetTargetSelector().behaviour == TargetBehaviour.First || _tower.GetTargetSelector().behaviour == TargetBehaviour.Last;
        _behaviourText.text = _tower.GetTargetSelector().GetBehaviour();
        _behaviourText.transform.localPosition = initPos + new Vector3(normalTarget ? 0 : offset, 0, 0);
        switch (_tower.GetTargetSelector().behaviour)
        {
            case TargetBehaviour.Health:
                _behaviourIconText.color = colorHealth;
                break;
            case TargetBehaviour.Armor:
                _behaviourIconText.color = colorArmor;
                break;
            case TargetBehaviour.Magic:
                _behaviourIconText.color = colorMagic;
                break;
        }
        
        _behaviourIconText.gameObject.SetActive(!normalTarget);
    }
}
