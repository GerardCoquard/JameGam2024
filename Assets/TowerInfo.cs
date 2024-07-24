using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _towerNameText;
    
    [SerializeField] Image _towerIcon;
    
    [SerializeField] TextMeshProUGUI _sellPriceText;
    
    [SerializeField] TextMeshProUGUI _damageText;
    [SerializeField] TextMeshProUGUI _fireRateText;
    [SerializeField] TextMeshProUGUI _rangeText;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] TextMeshProUGUI _armorText;
    [SerializeField] TextMeshProUGUI _magicText;

    public void SetData(Tower _tower)
    {
        _towerNameText.text = _tower.data.towerName;
        _towerIcon.sprite = _tower.data.icon;
        _sellPriceText.text = _tower.data.price.ToString();
        
        _damageText.text = _tower.baseDamage.ToString();
        _fireRateText.text = _tower.fireRate.ToString();
        _rangeText.text = _tower.range.ToString();
        _healthText.text = _tower.normalPenetration.ToString();
        _armorText.text = _tower.armorPenetration.ToString();
        _magicText.text = _tower.magicArmorPenetration.ToString();
    }
    
    
}
