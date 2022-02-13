using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public interface IStageManager
{
    RoomPoint GetRandomRoomCoordinate();
    RoomPoint GetRoomCoordinate(string roomName);
}

/*
 * Manages stage related behavior.
 * For example manage room coordinates, etc.
 */
public class StageManager : MonoBehaviour, IStageManager
{
    private Dictionary<string, RoomPoint> _roomPoints = new Dictionary<string, RoomPoint>();

    [SerializeField]
    private RoomPoint _roomPointPrefab;
    [SerializeField]
    private StageData _stageData;

    private void Awake()
    {
        Load();
    }

    #region Setter - Getter
    public RoomPoint GetRoomCoordinate(string roomName)
    {
        return _roomPoints[roomName];
    }

    public RoomPoint GetRandomRoomCoordinate()
    {
        RoomPoint randomRoom = Utils.Randomizer.GetRandomValue(_roomPoints);
        return randomRoom;
    }
    #endregion

    private void Load()
    {
        if (!_stageData) return;

        for (int i = 0; i < _stageData.Positions.Count; i++)
        {
            RoomPoint instance = Instantiate(_roomPointPrefab);
            instance.name = _stageData.Names[i];
            instance.transform.parent = transform;
            instance.Load(_stageData.Positions[i], _stageData.Names[i], _stageData.Rads[i]);
            _roomPoints.Add(instance.pointName, instance);
        }
    }
}
