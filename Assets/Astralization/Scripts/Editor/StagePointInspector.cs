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

    private StageBuilder _stageBuilder;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        _stageBuilder = current.GetComponentInParent<StageBuilder>();
        if (_stageBuilder == null) return;

        EditorGUILayout.Space();
        if (GUILayout.Button("Save"))
            _stageBuilder.AddCurrentStagePoint();
    }

}
