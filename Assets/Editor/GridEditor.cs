using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridMouse))]
public class GridEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridMouse grid = target as GridMouse;

        //grid.UpdateGrid();
        
    }

}
