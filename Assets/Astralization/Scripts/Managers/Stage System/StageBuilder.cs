using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

#region SerializableClass
[Serializable]
public class StagePointsDictionary: SerializableDictionary<string, StagePointFieldValue> { }
[Serializable]
public class TransitionZoneDictionary: SerializableDictionary<string, TransitionZoneFieldValue> { }
#endregion

/*
 * General builder for a stage.
 * Used with StageRoomInspector class for editor mode.
 */
public class StageBuilder : MonoBehaviour
{
    #region Variables
    [Header("Dictionaries")]
    [SerializeField]
    private StagePointsDictionary _stagePointsDict = new StagePointsDictionary();
    [SerializeField]
    private TransitionZoneDictionary _transitionZoneDict = new TransitionZoneDictionary();

    [Header("Prefab")]
    private StagePoint _stagePoint;
    private GhostTransitionZone _ghostTransitionZone;

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
    public void AddStagePoint(StagePointFieldValue fieldValue)
    {
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

    #region GhostTransitionZone
    public void AddTransitionZone(TransitionZoneFieldValue fieldValue)
    {
        if (_transitionZoneDict.ContainsKey(fieldValue.ZoneName)) _transitionZoneDict[fieldValue.ZoneName] = fieldValue;
        else _transitionZoneDict.Add(fieldValue.ZoneName, fieldValue);
    }
    public void DisplayTransitionZone(string zoneName)
    {
        if (zoneName == null) return;
        if (_ghostTransitionZone == null) _ghostTransitionZone = GetComponentInChildren<GhostTransitionZone>();

        if (_transitionZoneDict.ContainsKey(zoneName))
        {
            _ghostTransitionZone.Load(_transitionZoneDict[zoneName]);
            Debug.Log("[STAGE BUILDER] Ghost transition zone updated to display \"" + zoneName + "\" zone");
        }
        else Debug.Log("[STAGE BUILDER] Ghost transition zone with name: \"" + zoneName + "\" is not found");
    }
    #endregion

    #region Save Load
    private void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Astralization/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Astralization", "Resources");

        filePath += "/Stages";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Astralization/Resources", "Stages");
        AssetDatabase.Refresh();
    }

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

        string stagePointsFilename = string.Format("Assets/Astralization/Resources/Stages/{0}.asset", filename);
        AssetDatabase.CreateAsset(stagePoints, stagePointsFilename);
        return filename;
    }

    public string SaveTransitionZones(string filename)
    {
        _stageTransitionZoneData = null;

        StageTransitionZoneData stageZones = ScriptableObject.CreateInstance<StageTransitionZoneData>();
        stageZones.GhostTransitionZonePosition = new List<Vector3>(_transitionZoneDict.Count);
        stageZones.GhostTransitionZoneCenter = new List<Vector3>(_transitionZoneDict.Count);
        stageZones.GhostTransitionZoneSize = new List<Vector3>(_transitionZoneDict.Count);
        stageZones.GhostTransitionZoneEndpoint = new List<TransitionEndpointList>(_transitionZoneDict.Count);

        foreach (TransitionZoneFieldValue zone in _transitionZoneDict.Values)
        {
            stageZones.GhostTransitionZonePosition.Add(zone.LocalPosition);
            stageZones.GhostTransitionZoneCenter.Add(zone.ZoneCenter);
            stageZones.GhostTransitionZoneSize.Add(zone.ZoneSize);
            stageZones.GhostTransitionZoneEndpoint.Add(zone.EndpointList);
        }

        if (filename == null || filename == "") filename = string.Format("{0} - {1}", transform.root.name, "GhostTransitionZone");

        string stageZoneFilename = string.Format("Assets/Astralization/Resources/Stages/{0}.asset", filename);
        AssetDatabase.CreateAsset(stageZones, stageZoneFilename);
        return filename;
    }

    public (string,string) Save(string pointFilename, string transitionZoneFilename)
    {
        string filePath = Application.dataPath + "/Astralization/Resources/Stages";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        SaveStagePoints(pointFilename);
        SaveTransitionZones(transitionZoneFilename);
        return (pointFilename,transitionZoneFilename);
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
            Debug.Log("[STAGE BUILDER] Ghost Transition Zone data is not assigned");
            return;
        }

        _transitionZoneDict.Clear();

        for (int i = 0; i < _stageTransitionZoneData.GhostTransitionZonePosition.Count; i++)
        {
            string zoneName = GhostTransitionZone.GenerateZoneName(
                endpoints: _stageTransitionZoneData.GhostTransitionZoneEndpoint[i]
                );
            _transitionZoneDict.Add(zoneName, new TransitionZoneFieldValue(
                zoneName: zoneName,
                localPosition: _stageTransitionZoneData.GhostTransitionZonePosition[i],
                zoneCenter: _stageTransitionZoneData.GhostTransitionZoneCenter[i],
                zoneSize: _stageTransitionZoneData.GhostTransitionZoneSize[i],
                endpointList: _stageTransitionZoneData.GhostTransitionZoneEndpoint[i]
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
