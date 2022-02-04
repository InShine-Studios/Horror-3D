using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RoomCoordinate
{
    public string name;
    public Vector3 coordinate;
}

/**
 * StageController
 * Manages stage related behavior.
 * For example manage room coordinates, etc.
 */
public class StageController : MonoBehaviour
{
    public List<RoomCoordinate> RoomCoordinates;
    private List<string> _roomNames;
    private Dictionary<string, Vector3> _roomCoordDict = new Dictionary<string, Vector3>();

    private void Awake()
    {
        foreach (RoomCoordinate roomCoordinate in RoomCoordinates)
        {
            _roomCoordDict[roomCoordinate.name] = roomCoordinate.coordinate;
        }
        _roomNames = new List<string>(_roomCoordDict.Keys);
    }

    public Vector3 GetRoomCoordinate(string roomName)
    {
        return _roomCoordDict[roomName];
    }

    public Vector3 GetRandomRoomCoordinate()
    {
        int randomIdx = Utils.Randomizer.Rand.Next(RoomCoordinates.Count);
        string randomKey = _roomNames[randomIdx];
        return GetRoomCoordinate(randomKey);
    }
}
