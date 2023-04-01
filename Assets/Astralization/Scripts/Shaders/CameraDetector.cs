using System.Collections.Generic;
using UnityEngine;

namespace Astralization.Shaders
{
    public class CameraDetector : MonoBehaviour
    {
        #region Variables - Adjustable
        [SerializeField] private float _transparentScale;
        #endregion

        #region Variables - Process
        private Transform _player;
        private List<int> _fadeObjectListId;
        private List<int> _fadedObjectListId;
        private bool _processingTransparent = false;
        private bool _processingSolid = false;
        private bool _alreadyProcessTransparent = false;
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
            _player = transform.parent.parent.Find("Character");
        }

        private void Update()
        {
            DetectAllObjectsToPlayer();
            MakeTransparent();
            MakeSolid();
        }
        #endregion

        #region Detect Object
        private void DetectAllObjectsToPlayer()
        {
            _fadeObjectListId.Clear();

            float cameraToPlayerDistance = Vector3.Magnitude(transform.position - _player.position);

            Ray rayCameraToPlayer = new Ray(transform.position, _player.position - transform.position);
            Ray rayPlayerToCamera = new Ray(_player.position, transform.position - _player.position);

            RaycastHit[] rayCastCameraToPlayer = Physics.RaycastAll(rayCameraToPlayer, cameraToPlayerDistance);
            RaycastHit[] rayCastPlayerToCamera = Physics.RaycastAll(rayPlayerToCamera, cameraToPlayerDistance);

            foreach (RaycastHit obj in rayCastCameraToPlayer)
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

            foreach (RaycastHit obj in rayCastPlayerToCamera)
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

        #region Make Transparent and Solid
        private void MakeTransparent()
        {
            if (_alreadyProcessTransparent || _processingSolid)
            {
                return;
            }

            _alreadyProcessTransparent = true;
            _processingTransparent = true;
            foreach (int id in _fadeObjectListId.ToArray())
            {
                if (!_fadedObjectListId.Contains(id))
                {
                    GameObject obj = GameObject.Find(id.ToString());
                    foreach (Material mat in obj.GetComponent<Renderer>().materials)
                    {
                        Color objCol = mat.color;
                        Color newObjCol = new Color(objCol.r, objCol.g, objCol.b, _transparentScale);
                        mat.color = newObjCol;
                        mat.SetFloat("_Surface", (float)SurfaceType.Transparent);

                        SetupMaterialBlendMode(mat);
                    }

                    _fadedObjectListId.Add(id);
                }
            }
            _processingTransparent = false;
        }

        private void MakeSolid()
        {
            if (!_alreadyProcessTransparent || _processingTransparent)
            {
                return;
            }

            _alreadyProcessTransparent = false;
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
}