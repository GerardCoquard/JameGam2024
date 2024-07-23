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
    public static event Action<Vector3,string> OnGridClick;
    public static event Action OnExit;
    public static event Action<Vector3> OnStartDrag;
    public static event Action<Vector3> OnEndDrag;
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
        
        mousePos.z = cam.nearClipPlane;
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 1000, _clickLayerMask))
        {
            layer = LayerMask.LayerToName(hit.collider.gameObject.layer);
            pos = hit.point;
            return true;
        }
        return false;
    }

    public bool ClickOnGrid(out string layer, out Vector3 pos)
    {
        layer = "";
        pos = Vector3.zero;
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _sceneCamera.nearClipPlane;
        Ray ray = _sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, _clickLayerMask))
        {
            layer = LayerMask.LayerToName(hit.collider.gameObject.layer);
            pos = hit.point;
            return true;
        }
        return false;
    }

    private void CheckIfClickOnGrid()
    {
        if (IsPointerOverUI()) return;
        string layer;
        Vector3 pos;
        if(ClickOnGrid(out layer, out pos))
            OnGridClick?.Invoke(pos,layer);
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
        yield return new WaitUntil(() => Selection.objects.Length > 0);
        Selection.objects = Array.Empty<Object>();
    }
}
