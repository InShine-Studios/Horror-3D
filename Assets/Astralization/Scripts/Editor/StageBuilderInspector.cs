using UnityEngine;
using UnityEditor;

/*
 * The editor script to edit rooms of a stage.
 */
[CustomEditor(typeof(StageBuilder))]
public class StageBuilderInspector : Editor
{
    public StageBuilder current
    {
        get
        {
            return (StageBuilder)target;
        }
    }

    private string pointName;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        if (GUILayout.Button("Save All"))
            current.Save();
        if (GUILayout.Button("Load All"))
            current.Load();
        if (GUILayout.Button("Clear All Data"))
            current.ClearAll();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Stage Point", EditorStyles.boldLabel);
        pointName = GUILayout.TextField(pointName);
        if (GUILayout.Button("Display Stage Point"))
        {
            current.DisplayStagePoint(pointName);
        }

        if (GUILayout.Button("Save Stage Points"))
            current.SaveStagePoints();
        if (GUILayout.Button("Load Stage Points"))
            current.LoadStagePoints();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Ghost Transition Zone", EditorStyles.boldLabel);
        if (GUILayout.Button("Save Ghost Transition Zones"))
            current.SaveTransitionZones();
        if (GUILayout.Button("Load Ghost Transition Zones"))
            current.LoadTransitionZones();
        
    }

}
