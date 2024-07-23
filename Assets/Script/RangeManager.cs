using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeManager : MonoBehaviour
{
    public static RangeManager instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Show(Vector3 pos, float range)
    {
        transform.position = new Vector3(pos.x, 5, pos.z);
        transform.localScale = new Vector3(range, 15, range);
        gameObject.SetActive(true);
    }
    
    public void Attach(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void Hide()
    {
        transform.SetParent(null);
        gameObject.SetActive(false);
    }
}
