using UnityEngine;
using UnityEditor;

/*
 * The editor script to edit rooms of a stage.
 */
[CustomEditor(typeof(StageRoomBuilder))]
public class StageRoomInspector : Editor
{
    public StageRoomBuilder current
    {
        get
        {
            return (StageRoomBuilder)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Create Room Point"))
            current.CreateRoomPoints();
        if (GUILayout.Button("Create Ghost Transition Zone"))
            current.CreateGhostTransitionZone();
        if (GUILayout.Button("Clear All Child"))
            current.ClearAll();
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();
    }

}
