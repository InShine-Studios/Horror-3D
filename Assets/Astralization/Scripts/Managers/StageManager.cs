using System.Collections.Generic;
using UnityEngine;

public interface IStageManager 
{
    WorldPoint GetRandomRoomCoordinate();
    WorldPoint GetRoomCoordinate(string roomName);
}

/*
 * Manages stage related behavior.
 * For example manage room coordinates, etc.
 */
public class StageManager : MonoBehaviour, IStageManager
{
    #region Variables
    private Dictionary<string, WorldPoint> _roomPoints = new Dictionary<string, WorldPoint>();
    private Dictionary<string, GhostTransitionZone> _ghostTransitionZones = new Dictionary<string, GhostTransitionZone>();

    [SerializeField]
    private WorldPoint _roomPointPrefab;
    [SerializeField]
    private GhostTransitionZone _ghostTransitionZonePrefab;
    [SerializeField]
    private StageData _stageData;
    #endregion

    #region SetGet
    public WorldPoint GetRoomCoordinate(string roomName)
    {
        return _roomPoints[roomName];
    }

    public WorldPoint GetRandomRoomCoordinate()
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

        for (int i = 0; i < _stageData.Positions.Count; i++)
        {
            WorldPoint instance = Instantiate(_roomPointPrefab);
            instance.name = _stageData.Names[i];
            instance.transform.parent = transform;
            instance.Load(_stageData.Positions[i], _stageData.Names[i], _stageData.Rads[i]);
            _roomPoints.Add(instance.PointName, instance);
        }

        for (int i = 0; i < _stageData.GhostTransitionZonePosition.Count; i++)
        {
            GhostTransitionZone instance = Instantiate(_ghostTransitionZonePrefab);
            instance.transform.parent = transform;
            instance.Load(
                _stageData.GhostTransitionZonePosition[i], 
                _stageData.GhostTransitionZoneCenter[i],
                _stageData.GhostTransitionZoneSize[i], 
                _stageData.GhostTransitionZoneEndpoint[i].list);
            instance.SetZoneName();
            _ghostTransitionZones.Add(instance.name, instance);
        }
    }
    #endregion
}
