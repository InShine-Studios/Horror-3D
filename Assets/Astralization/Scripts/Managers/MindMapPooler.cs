using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MindMapPooler : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private MindMapNode _prefab;
    [SerializeField]
    private int _poolSize;
    [SerializeField]
    private bool _expandable = false;

    private ObjectPool<MindMapNode> _pool;
    #endregion

    #region SetGet
    public void GetInstance(out MindMapNode prefab)
    {
       _pool.Get(out prefab);
    }
    #endregion

    #region PoolManipulation
    public void Initialize()
    {
        _pool = new ObjectPool<MindMapNode>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _poolSize, _poolSize);
    }

    public void Initialize(MindMapNode prefab, int poolSize)
    {
        _prefab = prefab;
        _poolSize = poolSize;
        Initialize();
    }

    private MindMapNode CreatePooledObject()
    {
        MindMapNode instance = Instantiate(_prefab);
        instance.gameObject.SetActive(false);

        return instance;
    }

    public void ReturnObjectToPool(MindMapNode instance)
    {
        _pool.Release(instance);
    }

    private void OnTakeFromPool(MindMapNode instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.SetParent(transform, true);
    }

    private void OnReturnToPool(MindMapNode instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(MindMapNode instance)
    {
        Destroy(instance.gameObject);
    }
    #endregion
}