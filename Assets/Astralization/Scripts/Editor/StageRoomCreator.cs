using UnityEngine;
using UnityEditor;

/*
 * The editor script to edit rooms of a stage.
 */
[CustomEditor(typeof(StageManager))]
public class StageRoomCreator : Editor
{
    public StageManager current
    {
        get
        {
            return (StageManager)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Create"))
            current.Create();
        if (GUILayout.Button("Clear"))
            current.Clear();
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();
    }

}
