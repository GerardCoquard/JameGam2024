using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridManager _gridManager = (GridManager)target;
        
        if (GUILayout.Button("Reset Grid"))
        {
            _gridManager.FillGridWithGround();
        }
    }
}
