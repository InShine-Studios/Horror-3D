using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class StageRoomBuilder : MonoBehaviour
{
    [SerializeField]
    private RoomPoint _roomPointPrefab;
    private static Dictionary<string, RoomPoint> _roomPoints = new Dictionary<string, RoomPoint>();

    [SerializeField]
    private StageData _stageData;

    #region RoomPoints Setup
    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }

    public RoomPoint Create()
    {
        RoomPoint instance = Instantiate(_roomPointPrefab);
        instance.transform.parent = transform;
        return instance;
    }

    private void Rename()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.name = go.GetComponent<RoomPoint>().pointName;
        }
    }

    private void UpdatePoints()
    {
        // Reset dict first
        _roomPoints = new Dictionary<string, RoomPoint>();

        RoomPoint[] childPoints = transform.GetComponentsInChildren<RoomPoint>();
        foreach (RoomPoint r in childPoints)
        {
            _roomPoints.Add(r.pointName, r);
        }
    }
    #endregion

    #region Save - Load
    public void Save()
    {
        Rename();
        UpdatePoints();

        string filePath = Application.dataPath + "/Astralization/Resources/Stages";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        StageData stage = ScriptableObject.CreateInstance<StageData>();
        stage.Positions = new List<Vector3>(_roomPoints.Count);
        stage.Names = new List<string>(_roomPoints.Count);
        stage.Rads = new List<float>(_roomPoints.Count);

        foreach (RoomPoint r in _roomPoints.Values)
        {
            stage.Positions.Add(r.GetLocalPosition());
            stage.Names.Add(r.pointName);
            stage.Rads.Add(r.radius);
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

        //string fileName = string.Format("Assets/Astralization/Resources/Stages/{0}.asset", name);
        //StageData stageData = AssetDatabase.LoadAssetAtPath(fileName, typeof(StageData)) as StageData;

        if (!_stageData) return;

        for (int i = 0; i < _stageData.Positions.Count; i++)
        {
            RoomPoint r = Create();
            r.Load(_stageData.Positions[i], _stageData.Names[i], _stageData.Rads[i]);
        }

        Rename();
        UpdatePoints();
    }
    #endregion
}
