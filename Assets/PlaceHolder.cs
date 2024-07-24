using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    public static PlaceHolder instance;
    [SerializeField] private GameObject _cube;
    public LayerMask _layerMask;
    private bool active;

    private void Awake()
    {
        instance = this;
        InputManager.OnGridClick += (Vector3 x, string y) => Hide();
        InputManager.OnExit += Hide;
        _cube.SetActive(false);
    }

    private void FollowCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, _layerMask))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Wizards") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Path"))
                _cube.SetActive(false);
            else
            {
                _cube.transform.position = GridManager.instance.WorldPosToGridPos(hit.point);
                _cube.SetActive(true);
            }
        }
        else
            _cube.SetActive(false);
    }

    public void Show()
    {
        if (active) return;
        active = true;
        StartCoroutine(PlaceHolderFollowCursor());
    }

    public void Hide()
    {
        if (!active) return;
        active = false;
        _cube.SetActive(false);
        StopAllCoroutines();
    }

    IEnumerator PlaceHolderFollowCursor()
    {
        while (true)
        {
            FollowCursor();
            yield return null;
        }
    }
}
