using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardShop : MonoBehaviour
{
    [SerializeField] private TowerData _cardData;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _towerName;
    [SerializeField] private Image _icon;

    private void Awake()
    {
        TowersManager.OnPriceChange += UpdatePrice;
    }

    private void OnDestroy()
    {
        TowersManager.OnPriceChange -= UpdatePrice;
    }

    public void SetData(TowerData data)
    {
        _cardData = data;
        _price.text = _cardData.price.ToString() + "<sprite=0>";
        _towerName.text = _cardData.towerName;
        _icon.sprite = _cardData.icon;
    }

    public void UpdatePrice()
    {
        _price.text = TowersManager.GetPrice(_cardData.towerName) + "<sprite=0>";
    }
}
