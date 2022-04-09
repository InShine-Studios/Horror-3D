using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/*
 * General builder for a stage.
 * Used with StageRoomInspector class for editor mode.
 */
public class StageRoomBuilder : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private WorldPoint _roomPointPrefab;
    private static Dictionary<string, WorldPoint> _roomPoints = new Dictionary<string, WorldPoint>();

    [SerializeField]
    private GhostTransitionZone _ghostTransitionZonePrefab;
    private static Dictionary<string, GhostTransitionZone> _ghostTransitionZones = new Dictionary<string, GhostTransitionZone>();

    [SerializeField]
    private StageData _stageData;
    #endregion

    #region RoomPoints Setup
    public void ClearAll()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }

    public WorldPoint CreateRoomPoints()
    {
        WorldPoint instance = Instantiate(_roomPointPrefab);
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
                go.name = go.GetComponent<WorldPoint>().PointName;
            }
        }
    }

    private void UpdatePoints()
    {
        // Reset dict first
        _roomPoints = new Dictionary<string, WorldPoint>();

        WorldPoint[] childPoints = transform.GetComponentsInChildren<WorldPoint>();
        foreach (WorldPoint r in childPoints)
        {
            _roomPoints.Add(r.PointName, r);
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
    public void Save()
    {
        RenameRoomPoint();
        UpdatePoints();

        RenameEndpoints();
        UpdateTransitionEndpoints();
        _stageData = null;

        string filePath = Application.dataPath + "/Astralization/Resources/Stages";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        StageData stage = ScriptableObject.CreateInstance<StageData>();
        stage.Positions = new List<Vector3>(_roomPoints.Count);
        stage.Names = new List<string>(_roomPoints.Count);
        stage.Rads = new List<float>(_roomPoints.Count);

        stage.GhostTransitionZonePosition = new List<Vector3>(_ghostTransitionZones.Count);
        stage.GhostTransitionZoneCenter = new List<Vector3>(_ghostTransitionZones.Count);
        stage.GhostTransitionZoneSize = new List<Vector3>(_ghostTransitionZones.Count);
        stage.GhostTransitionZoneEndpoint = new List<TransitionEndpointList>(_ghostTransitionZones.Count);

        foreach (WorldPoint r in _roomPoints.Values)
        {
            stage.Positions.Add(r.GetLocalPosition());
            stage.Names.Add(r.PointName);
            stage.Rads.Add(r.Radius);
        }

        foreach (GhostTransitionZone zone in _ghostTransitionZones.Values)
        {
            stage.GhostTransitionZonePosition.Add(zone.GetZoneLocalPosition());
            stage.GhostTransitionZoneCenter.Add(zone.GetZoneColliderCenter());
            stage.GhostTransitionZoneSize.Add(zone.GetZoneColliderSize());
            stage.GhostTransitionZoneEndpoint.Add(zone.Endpoints);
        }

        string fileName = string.Format("Assets/Astralization/Resources/Stages/{1}.asset", filePath, name);
        AssetDatabase.CreateAsset(stage, fileName);
    }

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

    public void Load()
    {
        ClearAll();

        if (!_stageData) return;

        for (int i = 0; i < _stageData.Positions.Count; i++)
        {
            WorldPoint r = CreateRoomPoints();
            r.Load(_stageData.Positions[i], _stageData.Names[i], _stageData.Rads[i]);
        }

        for (int i = 0; i < _stageData.GhostTransitionZonePosition.Count; i++)
        {
            GhostTransitionZone zone = CreateGhostTransitionZone();
            zone.Load(
                _stageData.GhostTransitionZonePosition[i], 
                _stageData.GhostTransitionZoneCenter[i], 
                _stageData.GhostTransitionZoneSize[i], 
                _stageData.GhostTransitionZoneEndpoint[i].list
            );
        }

        RenameRoomPoint();
        UpdatePoints();

        RenameEndpoints();
        UpdateTransitionEndpoints();
    }
    #endregion
}
