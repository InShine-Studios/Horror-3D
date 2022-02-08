using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RoomCoordinate
{
    public string name;
    [DraggablePoint] public Vector3 coordinate;
    public float radius;
}

/*
 * StageController
 * Manages stage related behavior.
 * For example manage room coordinates, etc.
 */
public class StageController : MonoBehaviour
{
    public List<RoomCoordinate> RoomCoordinates;
    private static List<string> _roomNames;
    private static Dictionary<string, RoomCoordinate> _roomCoordDict = new Dictionary<string, RoomCoordinate>();

    private void Awake()
    {
        for (int i = 0; i < RoomCoordinates.Count; i++)
        {
            RoomCoordinate roomCoordinate = RoomCoordinates[i];
            roomCoordinate.coordinate += transform.position;
            _roomCoordDict[roomCoordinate.name] = roomCoordinate;
        }
        _roomNames = new List<string>(_roomCoordDict.Keys);
    }

    #region Setter - Getter
    public static List<string> GetRoomNames()
    {
        return _roomNames;
    }

    public static RoomCoordinate GetRoomCoordinate(string roomName)
    {
        return _roomCoordDict[roomName];
    }

    public static RoomCoordinate GetRandomRoomCoordinate()
    {
        int randomIdx = Utils.Randomizer.Rand.Next(_roomNames.Count);
        string randomKey = _roomNames[randomIdx];
        return GetRoomCoordinate(randomKey);
    }
    #endregion
}
