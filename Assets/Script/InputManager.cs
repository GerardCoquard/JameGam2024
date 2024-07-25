using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

[ExecuteAlways]
public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    
    [SerializeField] private Camera _sceneCamera;
    [SerializeField] private LayerMask _clickLayerMask;
    public static event Action<Vector3,string, bool> OnGridClick;
    public static event Action OnExit;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckIfClickOnGrid();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
            CancelAction();
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
    
    public bool ClickOnGridEditor(out string layer, out Vector3 pos, Vector3 mousePos, Camera cam)
    {
        layer = "";
        pos = Vector3.zero;
#if UNITY_EDITOR
        
        mousePos.z = cam.nearClipPlane;
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickLayerMask, QueryTriggerInteraction.Ignore))
        {
            layer = LayerMask.LayerToName(hit.collider.gameObject.layer);
            pos = hit.point;
            return true;
        }
#endif
        return false;
    }

    public bool ClickOnGrid(out string layer, out Vector3 pos, out bool plane)
    {
        layer = "";
        pos = Vector3.zero;
        plane = false;
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _sceneCamera.nearClipPlane;
        Ray ray = _sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickLayerMask, QueryTriggerInteraction.Ignore))
        {
            layer = LayerMask.LayerToName(hit.collider.gameObject.layer);
            pos = hit.point;
            plane = Vector3.Dot(hit.normal, Vector3.up) > 0.8f;
            return true;
        }
        return false;
    }

    private void CheckIfClickOnGrid()
    {
        if (IsPointerOverUI()) return;
        string layer;
        Vector3 pos;
        bool plane;
        if(ClickOnGrid(out layer, out pos, out plane))
            OnGridClick?.Invoke(pos,layer,plane);
    }

    public void CancelAction()
    {
        OnExit?.Invoke();
    }

    public void Deselect()
    {
        StartCoroutine(DeselectOnFrameEnd());
    }

    IEnumerator DeselectOnFrameEnd()
    {
        #if UNITY_EDITOR
        yield return new WaitUntil(() => Selection.objects.Length > 0);
        Selection.objects = Array.Empty<Object>();
#endif
        yield return null;
    }
}
