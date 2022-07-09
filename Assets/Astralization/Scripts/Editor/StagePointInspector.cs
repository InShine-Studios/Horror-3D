using UnityEngine;
using UnityEditor;

/*
 * The editor script to edit rooms of a stage.
 */
[CustomEditor(typeof(StagePoint))]
public class StagePointInspector : Editor
{
    public StagePoint current
    {
        get
        {
            return (StagePoint)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (current.transform.parent.GetComponent<StageBuilder>() == null) return;

        EditorGUILayout.Space();
        if (GUILayout.Button("Save"))
            current.Save();
    }

}
