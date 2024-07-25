using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        SetCurrency(GameManager.GetCurrency());
        GameManager.OnCurrencyChanged += SetCurrency;
    }

    private void SetCurrency(int amount)
    {
        _text.text = amount + "<sprite=0>";
    }
}
