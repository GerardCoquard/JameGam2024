using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridBuilder : EditorWindow
{
    private BuilderSelection _selection;
    private Dictionary<string, Texture2D> iconDictionary;
    private Dictionary<string, GameObject> prefabDictionary;
    
    [MenuItem("Window/Grid Builder")]
    public static void ShowWindow()
    {
        GetWindow<GridBuilder>("GridBuilder");
    }

    private void Awake()
    {
        iconDictionary = new Dictionary<string, Texture2D>();
        foreach (string name in Enum.GetNames(typeof(BuilderSelection)))
        {
            Texture2D tex = EditorGUIUtility.Load("Assets/Icons/"+ name+ ".png") as Texture2D;
            iconDictionary.Add(name, tex);
        }
        
        prefabDictionary = new Dictionary<string, GameObject>();
        foreach (string name in Enum.GetNames(typeof(BuilderSelection)))
        {
            GameObject go = EditorGUIUtility.Load("Assets/Icons/"+ name) as GameObject;
            prefabDictionary.Add(name, go);
        }
        
        position = new Rect (position.x, position.y, 250, 150);
    }
    
    private void OnGUI()
    {
        SetLabels();
        SetToggles();
    }
    
    private void SetLabels()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        foreach (string name in Enum.GetNames(typeof(BuilderSelection)))
        {
            GUILayout.Space(5);
            EditorGUILayout.LabelField(name + " Tile", EditorStyles.boldLabel, GUILayout.Width(75));
            GUILayout.Space(25);
        }
        
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    
    private void SetToggles()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        foreach (BuilderSelection value in Enum.GetValues(typeof(BuilderSelection)))
        {
            if (GUILayout.Toggle(_selection == value,iconDictionary[value.ToString()], GUILayout.Width(75), GUILayout.Height(75)))
            {
                _selection = value;
            }
            
            GUILayout.Space(25);
        }
        
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}



public enum BuilderSelection
{
    Grass,
    Path
}
