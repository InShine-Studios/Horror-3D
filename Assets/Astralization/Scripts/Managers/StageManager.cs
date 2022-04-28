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
    private StagePointsData _stagePointsData;
    [SerializeField]
    private StageTransitionZoneData _stageTransitionZoneData;
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
        if (!_stagePointsData) return;

        for (int i = 0; i < _stagePointsData.Positions.Count; i++)
        {
            WorldPoint instance = Instantiate(_roomPointPrefab);
            instance.name = _stagePointsData.Names[i];
            instance.transform.parent = transform;
            instance.Load(_stagePointsData.Positions[i], _stagePointsData.Names[i], _stagePointsData.Rads[i]);
            _roomPoints.Add(instance.PointName, instance);
        }

        if (!_stageTransitionZoneData) return;

        for (int i = 0; i < _stageTransitionZoneData.GhostTransitionZonePosition.Count; i++)
        {
            GhostTransitionZone instance = Instantiate(_ghostTransitionZonePrefab);
            instance.transform.parent = transform;
            instance.Load(
                _stageTransitionZoneData.GhostTransitionZonePosition[i],
                _stageTransitionZoneData.GhostTransitionZoneCenter[i],
                _stageTransitionZoneData.GhostTransitionZoneSize[i],
                _stageTransitionZoneData.GhostTransitionZoneEndpoint[i].list);
            instance.SetZoneName();
            _ghostTransitionZones.Add(instance.name, instance);
        }
    }
    #endregion
}
