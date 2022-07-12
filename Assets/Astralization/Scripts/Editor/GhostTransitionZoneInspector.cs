using UnityEngine;
using UnityEditor;
using System.Collections;

/*
 * The editor script to edit rooms of a stage.
 */
[CustomEditor(typeof(GhostTransitionZone))]
public class GhostTransitionZoneInspector : Editor
{
    public GhostTransitionZone current
    {
        get
        {
            return (GhostTransitionZone)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (current.transform.parent.GetComponent<StageBuilder>() == null) return;

        EditorGUILayout.Space();
        if (GUILayout.Button("Save"))
        {
            IEnumerator enumerator = current.Save().GetEnumerator();
            while(enumerator.MoveNext()) { }
        }
    }

}
