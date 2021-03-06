using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/*
 * General builder for a stage.
 * Used with StageRoomInspector class for editor mode.
 */
public class StageBuilder : MonoBehaviour
{
    #region Variables
    [Header("Prefab")]
    [SerializeField]
    private StagePoint _stagePointPrefab;
    private static Dictionary<string, StagePoint> _stagePoints = new Dictionary<string, StagePoint>();

    [SerializeField]
    private GhostTransitionZone _ghostTransitionZonePrefab;
    private static Dictionary<string, GhostTransitionZone> _ghostTransitionZones = new Dictionary<string, GhostTransitionZone>();

    [Space]
    [Header("StageData")]
    [SerializeField]
    private StagePointsData _stagePointsData;
    [SerializeField]
    private StageTransitionZoneData _stageTransitionZoneData;
    #endregion

    #region StagePoints Setup
    public void ClearAll()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }

    public StagePoint CreateStagePoint()
    {
        StagePoint instance = Instantiate(_stagePointPrefab);
        instance.transform.parent = transform;
        return instance;
    }

    private void RenameRoomPoint()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            GameObject go = transform.GetChild(i).gameObject;
            if (go.CompareTag("WorldPoint"))
            {
                go.name = go.GetComponent<StagePoint>().PointName;
            }
        }
    }

    private void UpdatePoints()
    {
        // Reset dict first
        _stagePoints = new Dictionary<string, StagePoint>();

        StagePoint[] childPoints = transform.GetComponentsInChildren<StagePoint>();
        foreach (StagePoint r in childPoints)
        {
            _stagePoints.Add(r.PointName, r);
        }
    }
    #endregion

    #region GhostTransitionZoneHandler
    public GhostTransitionZone CreateGhostTransitionZone()
    {
        GhostTransitionZone instance = Instantiate(_ghostTransitionZonePrefab);
        instance.transform.parent = transform;
        return instance;
    }

    private void RenameEndpoints()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            GameObject zoneGameObject = transform.GetChild(i).gameObject;
            if (zoneGameObject.CompareTag("GhostTransitionZone"))
            {
                zoneGameObject.GetComponent<GhostTransitionZone>().SetZoneName();
            }
        }
    }

    private void UpdateTransitionEndpoints()
    {
        // Reset dict first
        _ghostTransitionZones = new Dictionary<string, GhostTransitionZone>();

        GhostTransitionZone[] childZones = transform.GetComponentsInChildren<GhostTransitionZone>();
        foreach (GhostTransitionZone zone in childZones)
        {
            _ghostTransitionZones.Add(zone.name, zone);
        }
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

    public void SaveStagePoints()
    {
        RenameRoomPoint();
        UpdatePoints();
        _stagePointsData = null;

        StagePointsData stagePoints = ScriptableObject.CreateInstance<StagePointsData>();
        stagePoints.Positions = new List<Vector3>(_stagePoints.Count);
        stagePoints.Names = new List<string>(_stagePoints.Count);
        stagePoints.Rads = new List<float>(_stagePoints.Count);

        foreach (StagePoint r in _stagePoints.Values)
        {
            stagePoints.Positions.Add(r.GetLocalPosition());
            stagePoints.Names.Add(r.PointName);
            stagePoints.Rads.Add(r.Radius);
        }

        string stagePointsFilename = string.Format("Assets/Astralization/Resources/Stages/{0} - {1}.asset", name, "StagePoint");
        AssetDatabase.CreateAsset(stagePoints, stagePointsFilename);
    }

    public void SaveTransitionZones()
    {
        RenameEndpoints();
        UpdateTransitionEndpoints();
        _stageTransitionZoneData = null;

        StageTransitionZoneData stageZones = ScriptableObject.CreateInstance<StageTransitionZoneData>();
        stageZones.GhostTransitionZonePosition = new List<Vector3>(_ghostTransitionZones.Count);
        stageZones.GhostTransitionZoneCenter = new List<Vector3>(_ghostTransitionZones.Count);
        stageZones.GhostTransitionZoneSize = new List<Vector3>(_ghostTransitionZones.Count);
        stageZones.GhostTransitionZoneEndpoint = new List<TransitionEndpointList>(_ghostTransitionZones.Count);

        foreach (GhostTransitionZone zone in _ghostTransitionZones.Values)
        {
            stageZones.GhostTransitionZonePosition.Add(zone.GetZoneLocalPosition());
            stageZones.GhostTransitionZoneCenter.Add(zone.GetZoneColliderCenter());
            stageZones.GhostTransitionZoneSize.Add(zone.GetZoneColliderSize());
            stageZones.GhostTransitionZoneEndpoint.Add(zone.Endpoints);
        }

        string stageZoneFilename = string.Format("Assets/Astralization/Resources/Stages/{0} - {1}.asset", name, "GhostTransitionZone");
        AssetDatabase.CreateAsset(stageZones, stageZoneFilename);
    }

    public void Save()
    {
        string filePath = Application.dataPath + "/Astralization/Resources/Stages";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        SaveStagePoints();
        SaveTransitionZones();
    }

    public void LoadStagePoints()
    {
        if (!_stagePointsData) return;

        for (int i = 0; i < _stagePointsData.Positions.Count; i++)
        {
            StagePoint r = CreateStagePoint();
            r.Load(_stagePointsData.Positions[i], _stagePointsData.Names[i], _stagePointsData.Rads[i]);
        }

        RenameRoomPoint();
        UpdatePoints();
    }

    public void LoadTransitionZones()
    {
        if (!_stageTransitionZoneData) return;

        for (int i = 0; i < _stageTransitionZoneData.GhostTransitionZonePosition.Count; i++)
        {
            GhostTransitionZone zone = CreateGhostTransitionZone();
            zone.Load(
                _stageTransitionZoneData.GhostTransitionZonePosition[i],
                _stageTransitionZoneData.GhostTransitionZoneCenter[i],
                _stageTransitionZoneData.GhostTransitionZoneSize[i],
                _stageTransitionZoneData.GhostTransitionZoneEndpoint[i].list
            );
        }

        RenameEndpoints();
        UpdateTransitionEndpoints();
    }

    public void Load()
    {
        ClearAll();
        LoadStagePoints();
        LoadTransitionZones();
    }
    #endregion
}
