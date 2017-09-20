using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Minimap))]
public class MinimapEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Minimap mmap = target as Minimap;

        mmap.UpdateMap();


        
    }

}
