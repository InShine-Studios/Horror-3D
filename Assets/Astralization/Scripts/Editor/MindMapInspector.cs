using UnityEngine;
using UnityEditor;

/*
 * The editor script to edit clue nodes in Mind Map.
 */
[CustomEditor(typeof(MindMapBuilder))]
[ExecuteInEditMode]
public class MindMapBuilderInspector : Editor
{
    private string filename = "MindMapTree_Chapter1";

    public MindMapBuilder current
    {
        get
        {
            return (MindMapBuilder)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();
        if (GUILayout.Button("Add Chapter Node"))
            current.AddNode(MindMapNodeType.CHAPTER);
        if (GUILayout.Button("Add Core Node"))
            current.AddNode(MindMapNodeType.CORE);
        if (GUILayout.Button("Add Clue Node"))
            current.AddNode(MindMapNodeType.CLUE);
        if (GUILayout.Button("Clear"))
            current.ClearChild();

        EditorGUILayout.Space();
        filename = EditorGUILayout.TextField("Mind Map Filename: ", filename);
        if (GUILayout.Button("Save"))
            current.Save(filename);
        if (GUILayout.Button("Load"))
            filename = current.Load();
    }

}
