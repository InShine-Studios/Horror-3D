using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/*
 * Manages stage related behavior.
 * For example manage room coordinates, etc.
 */
public class StageManager : MonoBehaviour
{
    public List<RoomCoordinate> RoomCoordinates;
    private static List<string> _roomNames;
    private static Dictionary<string, RoomCoordinate> _roomCoordDict = new Dictionary<string, RoomCoordinate>();

    [SerializeField]
    private RoomPoint _roomPointPrefab;
    private static Dictionary<string, RoomPoint> _roomPoints = new Dictionary<string, RoomPoint>();

    [SerializeField]
    StageData stageDataDummy;

    private void Awake()
    {
        //for (int i = 0; i < RoomCoordinates.Count; i++)
        //{
        //    RoomCoordinate roomCoordinate = RoomCoordinates[i];
        //    roomCoordinate.coordinate += transform.position;
        //    _roomCoordDict[roomCoordinate.name] = roomCoordinate;
        //}
        //_roomNames = new List<string>(_roomCoordDict.Keys);
    }

    #region Setter - Getter
    //public static List<string> GetRoomNames()
    //{
    //    return _roomNames;
    //}

    //public static RoomCoordinate GetRoomCoordinate(string roomName)
    //{
    //    return _roomCoordDict[roomName];
    //}

    //public static RoomCoordinate GetRandomRoomCoordinate()
    //{
    //    int randomIdx = Utils.Randomizer.Rand.Next(_roomNames.Count);
    //    string randomKey = _roomNames[randomIdx];
    //    return GetRoomCoordinate(randomKey);
    //}
    #endregion

    #region RoomPoints Setup
    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
        // TODO: clear list?
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
            stage.Positions.Add(r.GetPosition());
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

        //if (stageData == null)
        //    return;

        string fileName = string.Format("Assets/Astralization/Resources/Stages/{0}.asset", name);
        StageData stageData = AssetDatabase.LoadAssetAtPath(fileName, typeof(StageData)) as StageData;

        for (int i = 0; i < stageData.Positions.Count; i++)
        {
            RoomPoint r = Create();
            r.Load(stageData.Positions[i], stageData.Names[i], stageData.Rads[i]);
        }

        Rename();
        UpdatePoints();
    }
    #endregion
}
