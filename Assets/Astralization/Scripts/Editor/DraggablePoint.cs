using System;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

//Handles relative to game object
public class DraggablePoint : PropertyAttribute { }


#if UNITY_EDITOR
[CustomEditor(typeof(StageController), true)]
/*
 * DraggablePointDrawer
 * Draw draggable point for Vector3, so determining position can be done by
 * dragging points in scene editor.
 */
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

    // Commented because code is cluttered and computational heavy
    //public void OnSceneGUI()
    //{
    //    SerializedProperty property = serializedObject.GetIterator();
    //    while (property.Next(true))
    //    {
    //        if (property.propertyType == SerializedPropertyType.Vector3)
    //        {
    //            handleVectorProperty(property);
    //        }
    //        else
    //        {
    //            for (int x = 0; x < _roomCoordinates.arraySize; x++)
    //            {
    //                SerializedProperty element = _roomCoordinates.GetArrayElementAtIndex(x);
    //                SerializedProperty _roomCoord = element.FindPropertyRelative("coordinate");
    //                SerializedProperty _roomName = element.FindPropertyRelative("name");
    //                handleVectorPropertyInArray(_roomCoord, _roomCoordinates, _roomName.stringValue);
    //            }
    //        }
    //    }

    //}

    //void handleVectorProperty(SerializedProperty property)
    //{
    //    FieldInfo field = serializedObject.targetObject.GetType().GetField(property.name);
    //    if (field == null)
    //    {
    //        return;
    //    }
    //    Handles.Label(property.vector3Value, property.name);
    //    property.vector3Value = Handles.PositionHandle(property.vector3Value, Quaternion.identity);
    //    serializedObject.ApplyModifiedProperties();
    //}

    //void handleVectorPropertyInArray(SerializedProperty property, SerializedProperty parent, string name)
    //{
    //    FieldInfo parentfield = serializedObject.targetObject.GetType().GetField(parent.name);
    //    if (parentfield == null)
    //    {
    //        return;
    //    }
    //    Handles.Label(property.vector3Value + ((MonoBehaviour)target).transform.position, parent.name + "[" + name + "]");
    //    property.vector3Value = Handles.PositionHandle(property.vector3Value + ((MonoBehaviour)target).transform.position, Quaternion.identity) - ((MonoBehaviour)target).transform.position;
    //    serializedObject.ApplyModifiedProperties();
    //}
}
#endif