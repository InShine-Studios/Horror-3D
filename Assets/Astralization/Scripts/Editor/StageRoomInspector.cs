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
        if (GUILayout.Button("Save Room Points"))
            current.SaveRoomPoints();
        if (GUILayout.Button("Load Room Points"))
            current.LoadRoomPoints();
        if (GUILayout.Button("Save Ghost Transition Zone"))
            current.SaveTransitionZones();
        if (GUILayout.Button("Load Ghost Transition Zone"))
            current.LoadTransitionZones();
        if (GUILayout.Button("Save All"))
            current.Save();
        if (GUILayout.Button("Load All"))
            current.Load();
    }

}
