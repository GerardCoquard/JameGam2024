using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _upgradeLevel;
    [SerializeField] TextMeshProUGUI _upgradeAmount;
    [SerializeField] TextMeshProUGUI _upgradeCost;

    public void SetData(int lvl, string amount, int cost)
    {
        _upgradeLevel.text = lvl.ToString();
        _upgradeCost.text = cost.ToString();
        _upgradeAmount.text = amount;
    }
}
