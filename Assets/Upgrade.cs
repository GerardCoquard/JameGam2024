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
        _upgradeLevel.text = "LVL " + lvl;
        _upgradeCost.text = cost + "<sprite=0>";
        _upgradeAmount.text = amount;
    }
}
