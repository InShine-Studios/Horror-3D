using System.Collections.Generic;
using UnityEngine;

public interface IStageManager
{
    StagePoint GetRandomRoomCoordinate();
    StagePoint GetRoomCoordinate(string roomName);
}

/*
 * Manages stage related behavior.
 * For example manage room coordinates, etc.
 */
public class StageManager : MonoBehaviour, IStageManager
{
    #region Variables
    private Dictionary<string, StagePoint> _stagePoints = new Dictionary<string, StagePoint>();
    private Dictionary<string, EnemyTransitionZone> _enemyTransitionZones = new Dictionary<string, EnemyTransitionZone>();

    [Header("Prefab")]
    [SerializeField]
    private StagePoint _stagePointPrefab;
    [SerializeField]
    private EnemyTransitionZone _enemyTransitionZonePrefab;

    [Header("Stage Data")]
    [SerializeField]
    private StagePointsData _stagePointsData;
    [SerializeField]
    private StageTransitionZoneData _stageTransitionZoneData;

    private static StageManager _instance;
    public static StageManager Instance
    {
        get { return _instance; }
    }
    #endregion

    #region Constructor
    private StageManager() { }
    #endregion

    #region SetGet
    public StagePoint GetRoomCoordinate(string roomName)
    {
        return _stagePoints[roomName];
    }

    public StagePoint GetRandomRoomCoordinate()
    {
        StagePoint randomRoom = Utils.Randomizer.GetRandomValue(_stagePoints);
        return randomRoom;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        Load();
        _instance = this;
    }
    #endregion

    #region Loader
    private void Load()
    {
        if (!_stagePointsData) return;

        for (int i = 0; i < _stagePointsData.Positions.Count; i++)
        {
            StagePoint instance = Instantiate(_stagePointPrefab);
            instance.name = _stagePointsData.Names[i];
            instance.transform.parent = transform;
            instance.Load(_stagePointsData.Positions[i], _stagePointsData.Names[i], _stagePointsData.Rads[i]);
            _stagePoints.Add(instance.PointName, instance);
        }

        if (!_stageTransitionZoneData) return;

        for (int i = 0; i < _stageTransitionZoneData.EnemyTransitionZonePosition.Count; i++)
        {
            EnemyTransitionZone instance = Instantiate(_enemyTransitionZonePrefab);
            instance.transform.parent = transform;
            instance.Load(
                _stageTransitionZoneData.EnemyTransitionZonePosition[i],
                _stageTransitionZoneData.EnemyTransitionZoneCenter[i],
                _stageTransitionZoneData.EnemyTransitionZoneSize[i],
                _stageTransitionZoneData.EnemyTransitionZoneEndpoint[i].list,
                renameEndpoint: true
                );
            instance.SetZoneName("Zone");
            _enemyTransitionZones.Add(instance.name, instance);
        }
    }
    #endregion
}
