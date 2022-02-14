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
    [SerializeField]
    private WorldPoint _roomPointPrefab;
    private static Dictionary<string, WorldPoint> _roomPoints = new Dictionary<string, WorldPoint>();

    [SerializeField]
    private StageData _stageData;

    #region RoomPoints Setup
    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }

    public WorldPoint Create()
    {
        WorldPoint instance = Instantiate(_roomPointPrefab);
        instance.transform.parent = transform;
        return instance;
    }

    private void Rename()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.name = go.GetComponent<WorldPoint>().PointName;
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

    #region Save - Load
    public void Save()
    {
        Rename();
        UpdatePoints();
        _stageData = null;

        string filePath = Application.dataPath + "/Astralization/Resources/Stages";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        StageData stage = ScriptableObject.CreateInstance<StageData>();
        stage.Positions = new List<Vector3>(_roomPoints.Count);
        stage.Names = new List<string>(_roomPoints.Count);
        stage.Rads = new List<float>(_roomPoints.Count);

        foreach (WorldPoint r in _roomPoints.Values)
        {
            stage.Positions.Add(r.GetLocalPosition());
            stage.Names.Add(r.PointName);
            stage.Rads.Add(r.Radius);
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
        Clear();

        if (!_stageData) return;

        for (int i = 0; i < _stageData.Positions.Count; i++)
        {
            WorldPoint r = Create();
            r.Load(_stageData.Positions[i], _stageData.Names[i], _stageData.Rads[i]);
        }

        Rename();
        UpdatePoints();
    }
    #endregion
}
