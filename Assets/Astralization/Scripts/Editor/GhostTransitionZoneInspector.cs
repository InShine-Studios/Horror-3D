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

    private StageBuilder _stageBuilder;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        _stageBuilder = current.GetComponentInParent<StageBuilder>();
        if (_stageBuilder == null) return;

        EditorGUILayout.Space();
        if (GUILayout.Button("Save"))
        {
            IEnumerator enumerator = _stageBuilder.AddCurrentTransitionZone().GetEnumerator();
            while (enumerator.MoveNext()) { }
        }
    }

}
