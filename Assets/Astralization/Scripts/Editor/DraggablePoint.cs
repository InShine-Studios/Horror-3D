using System;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

///Handles relative to game object
public class DraggablePoint : PropertyAttribute { }


#if UNITY_EDITOR
[CustomEditor(typeof(StageController), true)]
public class DraggablePointDrawer : Editor
{
    SerializedProperty _roomCoordinates;
    readonly GUIStyle style = new GUIStyle();

    void OnEnable()
    {
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;
        _roomCoordinates = serializedObject.FindProperty("RoomCoordinates");
    }

    public void OnSceneGUI()
    {
        for (int x = 0; x < _roomCoordinates.arraySize; x++)
        {
            SerializedProperty element = _roomCoordinates.GetArrayElementAtIndex(x);
            SerializedProperty _roomCoord = element.FindPropertyRelative("coordinate");
            handleVectorPropertyInArray(_roomCoord, _roomCoordinates, x);
        }
    }

    void handleVectorPropertyInArray(SerializedProperty property, SerializedProperty parent, int index)
    {
        FieldInfo parentfield = serializedObject.targetObject.GetType().GetField(parent.name);
        if (parentfield == null)
        {
            return;
        }
        var draggablePoints = parentfield.GetCustomAttributes(typeof(DraggablePoint), false);
        if (draggablePoints.Length > 0)
        {
            Handles.Label(property.vector3Value + ((MonoBehaviour)target).transform.position, parent.name + "[" + index + "]");
            property.vector3Value = Handles.PositionHandle(property.vector3Value + ((MonoBehaviour)target).transform.position, Quaternion.identity) - ((MonoBehaviour)target).transform.position;
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif