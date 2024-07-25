using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISpawner : MonoBehaviour
{
    public static UISpawner instance;
    [SerializeField] private GameObject _textPrefab;

    private void Awake()
    {
        instance = this;
    }

    public TextMeshProUGUI SpawnTextWithColor(Vector3 pos, string text, Color color)
    {
        TextMeshProUGUI textMeshProUGUI = Instantiate(_textPrefab, transform).GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = text;
        textMeshProUGUI.rectTransform.position = Camera.main.WorldToScreenPoint(pos);
        textMeshProUGUI.color = color;
        
        return textMeshProUGUI;
    }
    
    public TextMeshProUGUI SpawnTextWithColorFromUIPos(Vector3 pos, string text, Color color)
    {
        TextMeshProUGUI textMeshProUGUI = Instantiate(_textPrefab, transform).GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = text;
        textMeshProUGUI.color = color;
        textMeshProUGUI.rectTransform.position = pos;
        
        return textMeshProUGUI;
    }
    
}
