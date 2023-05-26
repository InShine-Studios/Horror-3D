using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Collections;

#region SerializableClass
[Serializable]
public class StagePointsDictionary : SerializableDictionary<string, StagePointFieldValue> { }
[Serializable]
public class TransitionZoneDictionary : SerializableDictionary<string, TransitionZoneFieldValue> { }
#endregion

/*
 * General builder for a stage.
 * Used with StageRoomInspector class for editor mode.
 */
public class StageBuilder : MonoBehaviour
{
    #region Const
    private readonly string[] DirectoryPathSequence = new string[] { "Resources", "Stages" };
    #endregion

    #region Variables
    [Header("Dictionaries")]
    [SerializeField]
    private StagePointsDictionary _stagePointsDict = new StagePointsDictionary();
    [SerializeField]
    private TransitionZoneDictionary _transitionZoneDict = new TransitionZoneDictionary();

    [Header("Prefab")]
    private StagePoint _stagePoint;
    private EnemyTransitionZone _enemyTransitionZone;

    [Space]
    [Header("StageData")]
    [SerializeField]
    private StagePointsData _stagePointsData;
    [SerializeField]
    private StageTransitionZoneData _stageTransitionZoneData;
    #endregion

    #region General
    public void ClearAll()
    {
        _stagePointsDict.Clear();
        _transitionZoneDict.Clear();
    }
    #endregion

    #region StagePoint
    public void AddCurrentStagePoint()
    {
        if (_stagePoint == null) _stagePoint = GetComponentInChildren<StagePoint>();

        StagePointFieldValue fieldValue = new StagePointFieldValue(_stagePoint.PointName, _stagePoint.transform.localPosition, _stagePoint.Radius);

        if (_stagePointsDict.ContainsKey(fieldValue.PointName)) _stagePointsDict[fieldValue.PointName] = fieldValue;
        else _stagePointsDict.Add(fieldValue.PointName, fieldValue);
    }

    public void DisplayStagePoint(string pointName)
    {
        if (pointName == null) return;
        if (_stagePoint == null) _stagePoint = GetComponentInChildren<StagePoint>();

        if (_stagePointsDict.ContainsKey(pointName))
        {
            _stagePoint.Load(_stagePointsDict[pointName]);
            Debug.Log("[STAGE BUILDER] Stage point updated to display \"" + pointName + "\" point");
        }
        else Debug.Log("[STAGE BUILDER] Stage point with name: \"" + pointName + "\" is not found");
    }
    #endregion

    #region EnemyTransitionZone
    public IEnumerable AddCurrentTransitionZone()
    {
        if (_enemyTransitionZone == null) _enemyTransitionZone = GetComponentInChildren<EnemyTransitionZone>();

        _enemyTransitionZone.Save();
        yield return new WaitUntil(() => !_enemyTransitionZone.IsSaving);

        TransitionZoneFieldValue fieldValue = _enemyTransitionZone.GetZoneFieldValue();

        if (_transitionZoneDict.ContainsKey(fieldValue.ZoneName)) _transitionZoneDict[fieldValue.ZoneName] = fieldValue;
        else _transitionZoneDict.Add(fieldValue.ZoneName, fieldValue);
    }
    public void DisplayTransitionZone(string zoneName)
    {
        if (zoneName == null) return;
        if (_enemyTransitionZone == null) _enemyTransitionZone = GetComponentInChildren<EnemyTransitionZone>();

        if (_transitionZoneDict.ContainsKey(zoneName))
        {
            _enemyTransitionZone.Load(_transitionZoneDict[zoneName]);
            Debug.Log("[STAGE BUILDER] Enemy transition zone updated to display \"" + zoneName + "\" zone");
        }
        else Debug.Log("[STAGE BUILDER] Enemy transition zone with name: \"" + zoneName + "\" is not found");
    }
    #endregion

    #region Save Load
    public string SaveStagePoints(string filename)
    {
        _stagePointsData = null;

        StagePointsData stagePoints = ScriptableObject.CreateInstance<StagePointsData>();
        stagePoints.Positions = new List<Vector3>(_stagePointsDict.Count);
        stagePoints.Names = new List<string>(_stagePointsDict.Count);
        stagePoints.Rads = new List<float>(_stagePointsDict.Count);

        foreach (StagePointFieldValue fieldValue in _stagePointsDict.Values)
        {
            stagePoints.Positions.Add(fieldValue.LocalPosition);
            stagePoints.Names.Add(fieldValue.PointName);
            stagePoints.Rads.Add(fieldValue.Radius);
        }

        if (filename == null || filename == "") filename = string.Format("{0} - {1}", transform.root.name, "StagePoint");

        Utils.FileSystemHelper.CreateAsset(stagePoints, DirectoryPathSequence, filename + ".asset");
        return filename;
    }

    public string SaveTransitionZones(string filename)
    {
        _stageTransitionZoneData = null;

        StageTransitionZoneData stageZones = ScriptableObject.CreateInstance<StageTransitionZoneData>();
        stageZones.EnemyTransitionZonePosition = new List<Vector3>(_transitionZoneDict.Count);
        stageZones.EnemyTransitionZoneCenter = new List<Vector3>(_transitionZoneDict.Count);
        stageZones.EnemyTransitionZoneSize = new List<Vector3>(_transitionZoneDict.Count);
        stageZones.EnemyTransitionZoneEndpoint = new List<TransitionEndpointList>(_transitionZoneDict.Count);

        foreach (TransitionZoneFieldValue zone in _transitionZoneDict.Values)
        {
            stageZones.EnemyTransitionZonePosition.Add(zone.LocalPosition);
            stageZones.EnemyTransitionZoneCenter.Add(zone.ZoneCenter);
            stageZones.EnemyTransitionZoneSize.Add(zone.ZoneSize);
            stageZones.EnemyTransitionZoneEndpoint.Add(zone.EndpointList);
        }

        if (filename == null || filename == "") filename = string.Format("{0} - {1}", transform.root.name, "EnemyTransitionZone");

        Utils.FileSystemHelper.CreateAsset(stageZones, DirectoryPathSequence, filename + ".asset");
        return filename;
    }

    public (string, string) Save(string pointFilename, string transitionZoneFilename)
    {
        Utils.FileSystemHelper.CreateDirectories(DirectoryPathSequence);

        SaveStagePoints(pointFilename);
        SaveTransitionZones(transitionZoneFilename);
        return (pointFilename, transitionZoneFilename);
    }

    public void LoadStagePoints()
    {
        if (!_stagePointsData)
        {
            Debug.Log("[STAGE BUILDER] Stage Point data is not assigned");
            return;
        }
        _stagePointsDict.Clear();

        for (int i = 0; i < _stagePointsData.Positions.Count; i++)
        {
            _stagePointsDict.Add(_stagePointsData.Names[i], new StagePointFieldValue(
                _stagePointsData.Names[i],
                _stagePointsData.Positions[i],
                _stagePointsData.Rads[i]
            ));
        }
    }

    public void LoadTransitionZones()
    {
        if (!_stageTransitionZoneData)
        {
            Debug.Log("[STAGE BUILDER] Enemy Transition Zone data is not assigned");
            return;
        }

        _transitionZoneDict.Clear();

        for (int i = 0; i < _stageTransitionZoneData.EnemyTransitionZonePosition.Count; i++)
        {
            string zoneName = EnemyTransitionZone.GenerateZoneName(
                endpoints: _stageTransitionZoneData.EnemyTransitionZoneEndpoint[i]
                );
            _transitionZoneDict.Add(zoneName, new TransitionZoneFieldValue(
                zoneName: zoneName,
                localPosition: _stageTransitionZoneData.EnemyTransitionZonePosition[i],
                zoneCenter: _stageTransitionZoneData.EnemyTransitionZoneCenter[i],
                zoneSize: _stageTransitionZoneData.EnemyTransitionZoneSize[i],
                endpointList: _stageTransitionZoneData.EnemyTransitionZoneEndpoint[i]
            ));
        }
    }

    public void Load()
    {
        ClearAll();
        LoadStagePoints();
        LoadTransitionZones();
    }
    #endregion
}
