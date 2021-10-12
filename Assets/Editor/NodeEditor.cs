using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    public Vector3 range;
    Node[] neighbours;

    public override void OnInspectorGUI()
    {
        Node _node = (Node)target;

        //_node.neighbours = EditorGUI.PropertyField(new Rect(), _node.neighbours);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("neighbours"), true);

        if (GUILayout.Button("Find Neigbours"))
        {
            _node.GetNeighbours();
        }

        if (GUILayout.Button("Delete Overlapped node"))
        {
            _node.DeleteIfOverlaped();
        }
    }

}
