using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MindMapPooler : MonoBehaviour
{
    #region Const
    private const string NodeModelName = "NodeModel";
    #endregion

    #region Variables
    [SerializeField]
    private MindMapNode _prefab;
    private NodeModelDictionary _nodeModelDictionary;
    [SerializeField]
    private int _poolSize;
    [SerializeField]
    private bool _expandable = false;

    private ObjectPool<MindMapNode> _pool;
    private ObjectPool<GameObject> _chapterNodeModelPool;
    private ObjectPool<GameObject> _coreNodeModelPool;
    private ObjectPool<GameObject> _clueNodeModelPool;
    #endregion

    #region SetGet
    public void GetInstance(out MindMapNode prefab)
    {
        GameObject nodeModel;
        _pool.Get(out prefab);
    }

    public void GetInstance(out MindMapNode prefab, MindMapNodeType nodeType, string layerName)
    {
        GameObject nodeModel;
        _pool.Get(out prefab);
        switch (nodeType)
        {
            case MindMapNodeType.CHAPTER:
                _chapterNodeModelPool.Get(out nodeModel);
                break;
            case MindMapNodeType.CORE:
                _coreNodeModelPool.Get(out nodeModel);
                break;
            case MindMapNodeType.CLUE:
                _clueNodeModelPool.Get(out nodeModel);
                break;
            default:
                _clueNodeModelPool.Get(out nodeModel);
                break;
        }
        nodeModel.transform.SetParent(prefab.transform, false);
        nodeModel.gameObject.layer = LayerMask.NameToLayer(layerName);
    }
    #endregion

    #region PoolInitialize
    public void Initialize()
    {
        _pool = new ObjectPool<MindMapNode>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _poolSize, _poolSize);
        _chapterNodeModelPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject nodeModel = Instantiate(_nodeModelDictionary[MindMapNodeType.CHAPTER]);
                nodeModel.name = NodeModelName;
                return nodeModel;
            },
            actionOnGet: OnTakeModelFromPool,
            actionOnRelease: OnReturnModelToPool,
            actionOnDestroy: OnDestroyModelObject,
            collectionCheck: false,
            defaultCapacity: 1,
            maxSize: 1);
        _coreNodeModelPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject nodeModel = Instantiate(_nodeModelDictionary[MindMapNodeType.CORE]);
                nodeModel.name = NodeModelName;
                return nodeModel;
            },
            actionOnGet: OnTakeModelFromPool,
            actionOnRelease: OnReturnModelToPool,
            actionOnDestroy: OnDestroyModelObject,
            collectionCheck: false,
            defaultCapacity: _poolSize,
            maxSize: _poolSize);
        _clueNodeModelPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject nodeModel = Instantiate(_nodeModelDictionary[MindMapNodeType.CLUE]);
                nodeModel.name = NodeModelName;
                return nodeModel;
            },
            actionOnGet: OnTakeModelFromPool,
            actionOnRelease: OnReturnModelToPool,
            actionOnDestroy: OnDestroyModelObject,
            collectionCheck: false,
            defaultCapacity: _poolSize,
            maxSize: _poolSize);
    }

    public void Initialize(MindMapNode prefab, NodeModelDictionary modelDict, int poolSize)
    {
        _prefab = prefab;
        _poolSize = poolSize;
        _nodeModelDictionary = modelDict;
        Initialize();
    }
    #endregion

    #region MindMapNodePoolManipulation
    private MindMapNode CreatePooledObject()
    {
        MindMapNode instance = Instantiate(_prefab);
        instance.gameObject.SetActive(false);

        return instance;
    }

    public void ReturnObjectToPool(MindMapNode instance)
    {
        GameObject nodeModel = instance.transform.Find(NodeModelName).gameObject;
        switch (instance.NodeType)
        {
            case MindMapNodeType.CHAPTER:
                _chapterNodeModelPool.Release(nodeModel);
                break;
            case MindMapNodeType.CORE:
                _coreNodeModelPool.Release(nodeModel);
                break;
            case MindMapNodeType.CLUE:
                _clueNodeModelPool.Release(nodeModel);
                break;
        }
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

    #region NodeModelManipulation
    private void OnTakeModelFromPool(GameObject instance)
    {
        instance.gameObject.SetActive(true);
    }

    private void OnReturnModelToPool(GameObject instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDestroyModelObject(GameObject instance)
    {
        Destroy(instance.gameObject);
    }
    #endregion
}