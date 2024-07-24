using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardShop : MonoBehaviour
{
    [SerializeField] private TowerData _cardData;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _towerName;
    [SerializeField] private Image _icon;
    private bool placing;

    public void SetData(TowerData data)
    {
        _cardData = data;
        _price.text = _cardData.price + "<sprite=0>";
        _towerName.text = _cardData.towerName;
        _icon.sprite = _cardData.icon;
    }

    public void TryUse()
    {
        GameManager.AddCurrency(4000);//DELETE
        if(GameManager.HaveCurrency(_cardData.price))
            StartTowerPlacement();
        else
        {
            UISpawner.instance.SpawnTextWithColorFromUIPos(transform.position,"Not enough currency!", Color.red);
            EventSystem.current.SetSelectedGameObject(null);
        }
            
    }

    private void StartTowerPlacement()
    {
        InputManager.OnGridClick += CheckPlacement;
        InputManager.OnExit += EndTowerPlacement;
        placing = true;
        PlaceHolder.instance.Show();
    }
    
    private void EndTowerPlacement()
    {
        if(!placing) return;
        InputManager.OnExit -= EndTowerPlacement;
        InputManager.OnGridClick -= CheckPlacement;
        placing = false;
    }

    private void CheckPlacement(Vector3 pos, string layer)
    {
        EndTowerPlacement();
        if (_cardData.towerName == "Purifier")
        {
            if (layer != "Ground" && layer != "PurifiedGround")
            {
                UISpawner.instance.SpawnTextWithColor(GridManager.instance.WorldPosToGridPos(pos),"Can't place here!", Color.white);
                return;
            }
            GameManager.RemoveCurrency(_cardData.price);
            GridManager.instance.Add(pos,_cardData.prefab,Tile.Wizard);
        }
        else
        {
            if (layer != "PurifiedGround")
            {
                UISpawner.instance.SpawnTextWithColor(GridManager.instance.WorldPosToGridPos(pos),"Can't place here!", Color.white);
                return;
            }
            GameManager.RemoveCurrency(_cardData.price);
            GridManager.instance.Add(pos,_cardData.prefab,Tile.Wizard);
        }
    }
}