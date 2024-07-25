using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridBuilder : EditorWindow
{
    private BuilderSelection _selection;
    private Dictionary<string, Texture2D> _iconDictionary;
    private Dictionary<string, GameObject> _prefabDictionary;
    private bool _drawing;
    private GameObject _selectedPrefab;

    [MenuItem("Window/Grid Builder")]
    public static void ShowWindow()
    {
        GetWindow<GridBuilder>("GridBuilder");
    }

    private void Awake()
    {
        _iconDictionary = new Dictionary<string, Texture2D>();
        foreach (string name in Enum.GetNames(typeof(BuilderSelection)))
        {
            Texture2D tex = EditorGUIUtility.Load("Assets/Prefabs/GridBuilder/Icons/"+ name+ ".png") as Texture2D;
            _iconDictionary.Add(name, tex);
        }
        
        _prefabDictionary = new Dictionary<string, GameObject>();
        foreach (string name in Enum.GetNames(typeof(BuilderSelection)))
        {
            if (name == BuilderSelection.Delete.ToString())
                continue;
            
            GameObject go = EditorGUIUtility.Load("Assets/Prefabs/GridBuilder/Tiles/"+ name + ".prefab") as GameObject;
            _prefabDictionary.Add(name, go);
        }
        
        position = new Rect (position.x, position.y, 250, 150);
    }

    private void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= SceneGUI;
        InputManager.instance.StopAllCoroutines();
    }

    private void OnEnable()
    {
        SceneView.onSceneGUIDelegate += SceneGUI;
        _selection = BuilderSelection.Ground;
        _drawing = false;
        InputManager.instance = FindObjectOfType<InputManager>();
        GridManager.instance = FindObjectOfType<GridManager>();
        GridManager.instance.GetHolders();
    }

    private void SceneGUI(SceneView sceneView)
    {
        Event cur = Event.current;
        if (cur.type == EventType.MouseDown && !cur.alt && cur.button == 0 && _drawing)
        {
            string layer;
            Vector3 pos;
            if(InputManager.instance.ClickOnGridEditor(out layer, out pos, cur.mousePosition, Camera.current))
                Draw(pos, layer);
        }

        if (cur.type == EventType.MouseUp && cur.button == 0 && _drawing)
            InputManager.instance.Deselect();
    }

    private void OnGUI()
    {
        SetToggles();
    }
    
    private void SetToggles()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        bool previousDrawing = _drawing;
        _drawing = EditorGUILayout.BeginToggleGroup("Draw", _drawing);
        
        foreach (BuilderSelection value in Enum.GetValues(typeof(BuilderSelection)))
        {
            if (value is BuilderSelection.Delete)
                continue;
            
            if (GUILayout.Toggle(_selection == value,_iconDictionary[value.ToString()], GUILayout.Width(75), GUILayout.Height(75)))
            {
                _selection = value;
                SetSelectedPrefab();
            }
            
            GUILayout.Space(25);
        }
        
        if (GUILayout.Toggle(_selection == BuilderSelection.Delete,_iconDictionary[BuilderSelection.Delete.ToString()], GUILayout.Width(75), GUILayout.Height(75)))
        {
            _selection = BuilderSelection.Delete;
            SetSelectedPrefab();
        }
        
        EditorGUILayout.EndToggleGroup();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    
    private void SetSelectedPrefab()
    {
        _selectedPrefab = _prefabDictionary[_selection.ToString()];
    }

    private void Draw(Vector3 pos, string layer)
    {
        if (layer == "Path") return;
        if(_selection == BuilderSelection.Ground || _selection == BuilderSelection.Path)
            GridManager.instance.Add(pos,_selectedPrefab,Tile.Ground);
        else
            GridManager.instance.Add(pos,_selectedPrefab,Tile.Decoration);
    }
}

public enum BuilderSelection
{
    Ground,
    Path,
    Delete
}
