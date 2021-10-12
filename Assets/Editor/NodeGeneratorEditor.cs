using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeGenerator))]
public class NodeGeneratorEditor : Editor
{  
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        NodeGenerator _parent = (NodeGenerator)target;        
        
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();

        if (GUILayout.Button("Search new nodes"))
        {
            _parent.SearchNewNodes();
        }
        if (GUILayout.Button("Find Neigbours In Children"))
        {
            _parent.FindNeighboursInChildren();
        }
        if (GUILayout.Button("Delete Overlapped Nodes"))
        {
            _parent.BorrarDuplicados();
        }

        GUILayout.Space(10);
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

    }
}
