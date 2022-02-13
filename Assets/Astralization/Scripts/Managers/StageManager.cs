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
    private Dictionary<string, WorldPoint> _roomPoints = new Dictionary<string, WorldPoint>();

    [SerializeField]
    private WorldPoint _roomPointPrefab;
    [SerializeField]
    private StageData _stageData;

    private void Awake()
    {
        Load();
    }

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
    }

    #region Setter - Getter
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

}
