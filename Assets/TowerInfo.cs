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
        _sellPriceText.text = _tower.data.price.ToString() + "<sprite=0>";
        
        _damageText.text = _tower.baseDamage.ToString();
        _fireRateText.text = _tower.fireRate.ToString();
        _rangeText.text = _tower.range.ToString();
        _healthText.text = "x" + _tower.normalPenetration;
        _armorText.text = "x" + _tower.armorPenetration;
        _magicText.text = "x" + _tower.magicArmorPenetration;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(_damageText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_fireRateText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rangeText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_healthText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_armorText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_magicText.rectTransform);
    }
    
    
}
