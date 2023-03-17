using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private int _poolSize;
    [SerializeField]
    private bool _expandable = false;

    private List<GameObject> _freeList = new List<GameObject>();
    private List<GameObject> _usedList = new List<GameObject>();
    #endregion

    #region SetGet
    public GameObject GetObjectFromPool()
    {
        int totalFree = _freeList.Count;
        if (totalFree == 0 && !_expandable) return null;
        else if (totalFree == 0) GenerateObject();

        GameObject gameObject = _freeList[totalFree - 1];
        _freeList.RemoveAt(totalFree - 1);
        _usedList.Add(gameObject);
        return gameObject;
    }
    #endregion

    private void Start()
    {
        //Initialize();
    }

    #region PoolManipulation
    public void Initialize(GameObject prefab, int poolSize)
    {
        _prefab = prefab;
        _poolSize = poolSize;
        Initialize();
    }

    private void Initialize()
    {
        _freeList.Clear();
        _usedList.Clear();

        for (int i = 0; i < _poolSize; i++)
        {
            GenerateObject();
        }
    }

    private void GenerateObject()
    {
        GameObject gameObject = Instantiate(_prefab);
        gameObject.transform.SetParent(transform, false);
        gameObject.SetActive(false);
        _freeList.Add(gameObject);
    }

    public void ReturnObject(GameObject gameObject)
    {
        Debug.Assert(_freeList.Contains(gameObject));
        gameObject.SetActive(true);
        _usedList.Remove(gameObject);
        _freeList.Add(gameObject);
    }
    #endregion
}