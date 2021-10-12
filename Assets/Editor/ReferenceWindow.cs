using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditorInternal;

public class ReferenceWindow : EditorWindow
{
    private GUIStyle titleText;
    private GUIStyle myStyle;
    private GUIStyle nodeText;

    private string currentName;

    searchType searchType;

    LayerMask filterMask;
    LayerMask tagMask;

    [MenuItem("CustomEditor/ReferenceWindow")]
    static void Init()
    {
        ReferenceWindow window = (ReferenceWindow)EditorWindow.GetWindow(typeof(ReferenceWindow));
        window.Show();
    }


    private void OnGUI()
    {
        WindowTitle();

        WindowContent();

    }

    private void WindowTitle()
    {
        EditorGUILayout.Space();

        titleText = new GUIStyle();
        titleText.fontStyle = FontStyle.Bold;
        titleText.alignment = TextAnchor.MiddleCenter;
        titleText.fontSize = 25;

        EditorGUILayout.LabelField("Prefab Finder", titleText);
        GUILayout.Space(15);
    }

    void WindowContent()
    {
        searchType = (searchType)EditorGUILayout.EnumPopup("Search Type", searchType);

        if (searchType == searchType.byComponent)
        {
            currentName = EditorGUILayout.TextField("Object", currentName);
        }
        else if (searchType == searchType.byLayer)
        {
            var tempMask = EditorGUILayout.MaskField("Layers", InternalEditorUtility.LayerMaskToConcatenatedLayersMask(filterMask), InternalEditorUtility.layers);
            filterMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
        }
        else if (searchType == searchType.byTag)
        {
            tagMask = EditorGUILayout.Popup("Tag", tagMask, InternalEditorUtility.tags);
        }

        GUILayout.Space(30f);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUI.Button(GUILayoutUtility.GetRect(200, 20), "Search"))
        {
            if (searchType == searchType.byComponent)
                FilterByComponent(currentName);
            else if (searchType == searchType.byLayer)
                FilterByLayer(filterMask);
            else if (searchType == searchType.byTag)
                FilterByTag(tagMask);

            AssetDatabase.Refresh();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    void FilterByComponent(string toSearch)
    {
        var guids = AssetDatabase.FindAssets("t:Prefab");

        var toSelect = new List<int>();
        foreach (var item in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(item);
            var toCheck = AssetDatabase.LoadAllAssetsAtPath(path);

            foreach (var check in toCheck)
            {
                var go = check as GameObject;

                if (go == null) continue;

                var comp = go.GetComponent(toSearch);
                if (comp != null)
                {
                    toSelect.Add(go.GetInstanceID());
                }
            }
            Selection.instanceIDs = new int[0];
            Selection.instanceIDs = toSelect.ToArray();
            ShowSelectionInProjecthierarchy();
        }
    }
    void FilterByLayer(LayerMask myMask)
    {
        var guids = AssetDatabase.FindAssets("t:Prefab");
        var toSelect = new List<int>();

        foreach (var item in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(item);
            var toCheck = AssetDatabase.LoadAllAssetsAtPath(path);

            foreach (var check in toCheck)
            {
                var go = check as GameObject;
                if (go == null) continue;

                var comp = go.layer;
                if (myMask == (myMask | (1 << comp)))
                {
                    toSelect.Add(go.GetInstanceID());
                }
            }
            Selection.instanceIDs = new int[0];
            Selection.instanceIDs = toSelect.ToArray();
            ShowSelectionInProjecthierarchy();
        }
    }
    void FilterByTag(LayerMask tag)
    {
        var guids = AssetDatabase.FindAssets("t:Prefab");
        var toSelect = new List<int>();

        foreach (var item in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(item);
            var toCheck = AssetDatabase.LoadAllAssetsAtPath(path);

            foreach (var check in toCheck)
            {
                var go = check as GameObject;
                if (go == null) continue;
                                
                if (go.CompareTag(InternalEditorUtility.tags[tagMask]))
                    {
                    toSelect.Add(go.GetInstanceID());
                }
            }
            Selection.instanceIDs = new int[0];
            ShowSelectionInProjecthierarchy();
            Selection.instanceIDs = toSelect.ToArray();
            ShowSelectionInProjecthierarchy();
        }

    }



    void ShowSelectionInProjecthierarchy()
    {
        var pbType = GetType("UnityEditor.ProjectBrowser");
        var meth = pbType.GetMethod("ShowSelectedObjectsInLastInteractedProjectBrowser", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        meth.Invoke(null, null);       
    }
    Type GetType(string name)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var item in assemblies)
        {
            var type = item.GetType(name);
            if (type != null)
            {
                return type;
            }
        }
        return null;
    }
}

enum searchType
{
    byComponent,
    byLayer,
    byTag
}
