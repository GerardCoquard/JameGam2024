using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISpawn : MonoBehaviour
{
    [SerializeField] private float _timeOnScreen;
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private float _speed;
    private TextMeshProUGUI _text;
    

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut());
    }

    private void Update()
    {
        transform.position = transform.position + new Vector3(0, _speed * Time.deltaTime, 0);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(_timeOnScreen);
        
        float time = 0;
        Color initColor = _text.color;
        Color finalColor = initColor;
        finalColor.a = 0;
        while (time < _fadeOutTime)
        {
            _text.color = Color.Lerp(initColor, finalColor, time / _fadeOutTime);
            time += Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
