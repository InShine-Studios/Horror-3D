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
    private string zoneName;
    private string pointFilename;
    private string transitionZoneFilename;

    public void SetPointFileName(string pointFilename)
    {
        this.pointFilename = pointFilename;
    }

    public void SetTransitionZoneFilename(string transitionZoneFilename)
    {
        this.transitionZoneFilename = transitionZoneFilename;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Filename");
        pointFilename = EditorGUILayout.TextField("Point Filename: ", pointFilename);
        transitionZoneFilename = EditorGUILayout.TextField("TransitionZone Filename: ", transitionZoneFilename);
        if (GUILayout.Button("Save All"))
            (pointFilename, transitionZoneFilename) = current.Save(pointFilename, transitionZoneFilename);
        if (GUILayout.Button("Load All"))
            current.Load();
        if (GUILayout.Button("Clear All Data"))
            current.ClearAll();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Stage Point", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Display");
        pointName = EditorGUILayout.TextField("Point name: ", pointName);
        if (GUILayout.Button("Display Stage Point"))
        {
            current.DisplayStagePoint(pointName);
        }

        EditorGUILayout.LabelField("Save - Load");
        if (GUILayout.Button("Save Stage Points"))
            pointFilename = current.SaveStagePoints(pointFilename);
        if (GUILayout.Button("Load Stage Points"))
            current.LoadStagePoints();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Ghost Transition Zone", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Display");
        zoneName = EditorGUILayout.TextField("Point name: ", zoneName);
        if (GUILayout.Button("Display Stage Point"))
        {
            current.DisplayTransitionZone(zoneName);
        }

        EditorGUILayout.LabelField("Save - Load");
        if (GUILayout.Button("Save Ghost Transition Zones"))
            transitionZoneFilename = current.SaveTransitionZones(transitionZoneFilename);
        if (GUILayout.Button("Load Ghost Transition Zones"))
            current.LoadTransitionZones();
        
    }

}
