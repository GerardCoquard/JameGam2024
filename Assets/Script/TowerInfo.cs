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

    [SerializeField] private Button _sellButton;

    public void SetData(Tower _tower)
    {
        _towerNameText.text = _tower.towerName;
        _towerIcon.sprite = _tower.icon;
        
        _damageText.text = _tower.damage.ToString();
        _fireRateText.text = _tower.fireRate.ToString("F1");
        _rangeText.text = _tower.range.ToString("F1");
        _healthText.text = "x" + _tower.normalPenetration;
        _armorText.text = "x" + _tower.armorPenetration;
        _magicText.text = "x" + _tower.magicArmorPenetration;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(_damageText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_fireRateText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rangeText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_healthText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_armorText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_magicText.rectTransform);

        if (_tower.towerName == "Purifier")
        {
            _sellPriceText.text = "Can't be sold";
            _sellButton.interactable = false;
        }
        else
        {
            _sellPriceText.text = _tower.basePrice + "<sprite=0>";
            _sellButton.interactable = true;
        }
            
    }
    
    
}
