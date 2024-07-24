using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Animator _shopAnimator;
    [SerializeField] private Transform _arrow1;
    [SerializeField] private Transform _arrow2;
    [SerializeField] private TextMeshProUGUI _buttonText;
    
    private bool _shopOpen;

    private void Awake()
    {
        _shopOpen = true;
        SetVisuals();
        
        foreach (TowerData tower in GameManager.gameData.towers)
        {
            CardShop card = Instantiate(_cardPrefab, transform).GetComponent<CardShop>();
            card.SetData(tower);
        }
    }

    public void ShopButton()
    {
        _shopOpen = !_shopOpen;
        SetVisuals();
        //Stop doing actions
    }

    private void SetVisuals()
    {
        _shopAnimator.SetBool("Open", _shopOpen);
        _buttonText.text = _shopOpen ? "Close" : "Open";
        _arrow1.localRotation = Quaternion.Euler(0,0,_shopOpen ? -90 : 90);
        _arrow2.localRotation = Quaternion.Euler(0,0,_shopOpen ? -90 : 90);

    }
}
