using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    #region Variables - Adjustable
    [SerializeField] private Transform _player;
    [SerializeField] private float _transparantScale;
    #endregion

    #region Variables - Process
    private List<int> _fadeObjectListId;
    private List<int> _fadedObjectListId;
    private bool _processingTransparant = false;
    private bool _processingSolid = false;
    private bool _alreadyProcessTransparant = false;
    private Transform _camera;
    public enum SurfaceType
    {
        Opaque,
        Transparent
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _fadeObjectListId = new List<int>();
        _fadedObjectListId = new List<int>();

        _camera = transform;
    }

    private void Update()
    {
        DetectAllObjectsToPlayer();
        MakeTransparant();
        MakeSolid();
    }
    #endregion

    #region Detect Object
    private void DetectAllObjectsToPlayer()
    {
        _fadeObjectListId.Clear();

        float cameraToPlayerDistance = Vector3.Magnitude(_camera.position - _player.position);

        Ray rayCameraToPlayer = new Ray(_camera.position, _player.position - _camera.position);
        Ray rayPlayerToCamera = new Ray(_player.position, _camera.position - _player.position);

        var rayCastCameraToPlayer = Physics.RaycastAll(rayCameraToPlayer, cameraToPlayerDistance);
        var rayCastPlayerToCamera = Physics.RaycastAll(rayPlayerToCamera, cameraToPlayerDistance);

        foreach (var obj in rayCastCameraToPlayer)
        {
            if (obj.collider.CompareTag("FadeProperty"))
            {
                int objId = obj.collider.GetInstanceID();
                if (!_fadeObjectListId.Contains(objId))
                {
                    _fadeObjectListId.Add(objId);
                    obj.transform.gameObject.name = objId.ToString();
                }
            }
        }

        foreach (var obj in rayCastPlayerToCamera)
        {
            if (obj.collider.CompareTag("FadeProperty"))
            {
                int objId = obj.collider.GetInstanceID();
                if (!_fadeObjectListId.Contains(objId))
                {
                    _fadeObjectListId.Add(objId);
                    obj.transform.gameObject.name = objId.ToString();
                }
            }
        }
    }
    #endregion

    #region Make Transparant and Solid
    private void MakeTransparant()
    {
        if (_alreadyProcessTransparant || _processingSolid)
        {
            return;
        }

        _alreadyProcessTransparant = true;
        _processingTransparant = true;
        foreach (int id in _fadeObjectListId.ToArray())
        {
            if (!_fadedObjectListId.Contains(id))
            {
                GameObject obj = GameObject.Find(id.ToString());
                foreach (Material mat in obj.GetComponent<Renderer>().materials)
                {
                    Color objCol = mat.color;
                    Color newObjCol = new Color(objCol.r, objCol.g, objCol.b, _transparantScale);
                    mat.color = newObjCol;
                    mat.SetFloat("_Surface", (float)SurfaceType.Transparent);

                    SetupMaterialBlendMode(mat);
                }

                _fadedObjectListId.Add(id);
            }
        }
        _processingTransparant = false;
    }

    private void MakeSolid()
    {
        if (!_alreadyProcessTransparant || _processingTransparant)
        {
            return;
        }

        _alreadyProcessTransparant = false;
        _processingSolid = true;
        foreach (int id in _fadedObjectListId.ToArray())
        {
            if (!_fadeObjectListId.Contains(id))
            {
                GameObject obj = GameObject.Find(id.ToString());
                foreach (Material mat in obj.GetComponent<Renderer>().materials)
                {
                    Color objCol = mat.color;
                    Color newObjCol = new Color(objCol.r, objCol.g, objCol.b, 1);
                    mat.color = newObjCol;
                    mat.SetFloat("_Surface", (float)SurfaceType.Opaque);

                    SetupMaterialBlendMode(mat);
                }

                _fadedObjectListId.Remove(id);
            }

        }
        _processingSolid = false;
    }


    void SetupMaterialBlendMode(Material material)
    {
        bool alphaClip = material.GetFloat("_AlphaClip") == 1;
        if (alphaClip)
            material.EnableKeyword("_ALPHATEST_ON");
        else
            material.DisableKeyword("_ALPHATEST_ON");

        SurfaceType surfaceType = (SurfaceType)material.GetFloat("_Surface");
        if (surfaceType == (float)SurfaceType.Opaque)
        {
            material.SetOverrideTag("RenderType", "");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
            material.SetShaderPassEnabled("ShadowCaster", true);
        }
        else
        {
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            material.SetShaderPassEnabled("ShadowCaster", false);
        }
    }
    #endregion
}
