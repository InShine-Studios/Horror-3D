using System.Collections.Generic;
using UnityEngine;

public interface IStageManager { }

/*
 * Manages stage related behavior.
 * For example manage room coordinates, etc.
 */
public class StageManager : MonoBehaviour, IStageManager
{
    #region Variables
    private static Dictionary<string, WorldPoint> _roomPoints = new Dictionary<string, WorldPoint>();

    [SerializeField]
    private WorldPoint _roomPointPrefab;
    [SerializeField]
    private StageData _stageData;
    #endregion

    #region SetGet
    public static WorldPoint GetRoomCoordinate(string roomName)
    {
        Debug.Log(_roomPoints);
        return _roomPoints[roomName];
    }

    public static WorldPoint GetRandomRoomCoordinate()
    {
        WorldPoint randomRoom = Utils.Randomizer.GetRandomValue(_roomPoints);
        return randomRoom;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        Load();
    }
    #endregion

    #region Loader
    private void Load()
    {
        if (!_stageData) return;
        if (_roomPoints.Count > 0) _roomPoints.Clear();

        for (int i = 0; i < _stageData.Positions.Count; i++)
        {
            WorldPoint instance = Instantiate(_roomPointPrefab);
            instance.name = _stageData.Names[i];
            instance.transform.parent = transform;
            instance.Load(_stageData.Positions[i], _stageData.Names[i], _stageData.Rads[i]);
            _roomPoints.Add(instance.PointName, instance);
        }
    }
    #endregion
}
