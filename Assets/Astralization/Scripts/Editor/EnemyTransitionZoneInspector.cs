using UnityEngine;
using UnityEditor;
using System.Collections;

/*
 * The editor script to edit rooms of a stage.
 */
[CustomEditor(typeof(EnemyTransitionZone))]
public class EnemyTransitionZoneInspector : Editor
{
    public EnemyTransitionZone current
    {
        get
        {
            return (EnemyTransitionZone)target;
        }
    }

    private StageBuilder _stageBuilder;
    private string _name;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        _stageBuilder = current.GetComponentInParent<StageBuilder>();
        if (_stageBuilder == null) return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Current Transition Zone: " + current.GetZoneName());
        if (GUILayout.Button("Save"))
        {
            IEnumerator enumerator = _stageBuilder.AddCurrentTransitionZone().GetEnumerator();
            while (enumerator.MoveNext()) { }
        }
    }

}