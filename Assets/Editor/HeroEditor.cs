using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Hero))]
public class HeroEditor : Editor
{
    private Hero hero;
    private GUIStyle labelStyle;

    int customExp;

    private void OnEnable()
    {
        hero = (Hero)target;

    }
    public override void OnInspectorGUI()
    {
        hero.heroName = EditorGUILayout.TextField("Name: ", hero.heroName);

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Label("Speed: ");
        hero.speed = GUILayout.HorizontalSlider(hero.speed, 0, 10);
        GUILayout.EndVertical();
        hero.speed = EditorGUILayout.FloatField(hero.speed, GUILayout.Width(100));
        GUILayout.EndHorizontal();
               
        GUILayout.BeginHorizontal();
        GUILayout.Label("Level: " + hero.level);
        if (GUILayout.Button("Reset"))
        {
            hero.ammountOfExperience = 0;
            hero.level = 0;
        }
        GUILayout.EndHorizontal();
        hero.XPtoLevelUp = EditorGUILayout.FloatField("Exp. to level up: ", hero.XPtoLevelUp);

        customExp = EditorGUILayout.IntField("Custom Xp: ", customExp);

        EditorGUI.ProgressBar(GUILayoutUtility.GetRect(15, 15), hero.ammountOfExperience / hero.XPtoLevelUp, "" + hero.ammountOfExperience / hero.XPtoLevelUp * 100 + "%");

        if (Application.isPlaying)
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            if (GUILayout.Button("Add custom XP"))
            {
                hero.ammountOfExperience += customExp;
            }
            if (GUILayout.Button("Add 10% XP"))
            {
                hero.ammountOfExperience += hero.XPtoLevelUp / 10;
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            if (GUILayout.Button("Add 50% XP"))
            {
                hero.ammountOfExperience += hero.XPtoLevelUp / 2;
            }
            if (GUILayout.Button("Add 100% XP"))
            {
                hero.ammountOfExperience += hero.XPtoLevelUp;
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        if (Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Estas en Play Mode, los cambios que realices no se guardaran", MessageType.Warning);
            return;
        }


    }

}
