using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;

    private void Awake()
    {
        foreach (TowerData tower in GameManager.gameData.towers)
        {
            CardShop card = Instantiate(_cardPrefab, transform).GetComponent<CardShop>();
            card.SetData(tower);
        }
    }
}
