using Astralization.Utils.Calculation;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Astralization.Managers.StageSystem
{
    #region SerializableClass
    [Serializable]
    public class TransitionEndpointList
    {
        public List<TransitionEndpoint> list;

        public TransitionEndpointList(List<TransitionEndpoint> list)
        {
            this.list = list;
        }
    }

    [Serializable]
    public class TransitionEndpoint
    {
        public string AreaName;
        public Vector3 Position;

        public TransitionEndpoint(string areaName, Vector3 position)
        {
            this.AreaName = areaName;
            this.Position = position;
        }
    }
    [Serializable]
    public class TransitionZoneFieldValue
    {
        public string ZoneName;
        public Vector3 LocalPosition;
        public Vector3 ZoneCenter;
        public Vector3 ZoneSize;
        public TransitionEndpointList EndpointList;

        public TransitionZoneFieldValue(string zoneName, Vector3 localPosition, Vector3 zoneCenter, Vector3 zoneSize, TransitionEndpointList endpointList)
        {
            this.ZoneName = zoneName;
            this.LocalPosition = localPosition;
            this.ZoneCenter = zoneCenter;
            this.ZoneSize = zoneSize;
            this.EndpointList = endpointList;
        }
    }
    #endregion

    public class GhostTransitionZone : MonoBehaviour
    {
        #region Variables
        private TransitionEndpointList Endpoints;
        private BoxCollider _boxCollider;
        private bool _isUpdatingEndpoint = false;
        private bool _isSaving = false;
        public bool IsSaving { get { return _isSaving; } }
        private TransitionZoneFieldValue _zoneFieldValue;

        public Transform EnterPoint { get; private set; }
        public Transform ExitPoint { get; private set; }
        #endregion

        #region SetGet
        public static string GenerateZoneName(TransitionEndpointList endpoints, string prefix = "")
        {
            if (endpoints == null) return "";

            string zoneName = prefix;
            if (prefix != "") zoneName += " ";

            for (int i = 0; i < endpoints.list.Count; i++)
            {
                zoneName += endpoints.list[i].AreaName;
                if (i < endpoints.list.Count - 1) zoneName += " - ";
            }
            return zoneName;
        }
        public void SetZoneName(string prefix = "")
        {
            name = GenerateZoneName(Endpoints, prefix);
        }

        public TransitionZoneFieldValue GetZoneFieldValue()
        {
            return _zoneFieldValue;
        }

        public string GetZoneName()
        {
            return GenerateZoneName(Endpoints);
        }
        #endregion

        #region MonoBehaviour
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ghost"))
            {
                int endpointIndex = GetEnteringPointIndex(other.transform.position);
                EnterPoint = transform.GetChild(endpointIndex);
                ExitPoint = transform.GetChild(MathCalcu.mod(endpointIndex + 1, 2));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ghost"))
            {
                EnterPoint = null;
                ExitPoint = null;
            }
        }
        #endregion

        #region SaveLoad
        public void Save()
        {
            _isSaving = true;
            if (_boxCollider == null) _boxCollider = GetComponent<BoxCollider>();

            // Update Endpoints
            GhostTransitionZoneEndpoint[] ghostTransitionZoneEndpoints = GetComponentsInChildren<GhostTransitionZoneEndpoint>();
            List<TransitionEndpoint> transitionEndpoints = new List<TransitionEndpoint>();
            for (int i = 0; i < ghostTransitionZoneEndpoints.Length; i++)
            {
                transitionEndpoints.Add(new TransitionEndpoint(ghostTransitionZoneEndpoints[i].AreaName, ghostTransitionZoneEndpoints[i].transform.localPosition));
            }
            Endpoints = new TransitionEndpointList(transitionEndpoints);

            _zoneFieldValue = new TransitionZoneFieldValue(
                GenerateZoneName(Endpoints),
                transform.localPosition,
                _boxCollider.center,
                _boxCollider.size,
                Endpoints
            );

            _isSaving = false;
        }

        private void UpdateEndpoints(bool renameEndppoint = false)
        {
            GhostTransitionZoneEndpoint[] transitionZoneEndpoints = GetComponentsInChildren<GhostTransitionZoneEndpoint>();
            for (int i = 0; i < Endpoints.list.Count; i++)
            {
                transitionZoneEndpoints[i].AreaName = Endpoints.list[i].AreaName;
                transitionZoneEndpoints[i].transform.localPosition = Endpoints.list[i].Position;
                if (renameEndppoint) transitionZoneEndpoints[i].name = Endpoints.list[i].AreaName;
            }
        }

        public void Load(Vector3 zoneLocalPos, Vector3 zoneCenter, Vector3 zoneSize, List<TransitionEndpoint> endpoints, bool renameEndpoint = false)
        {
            transform.localPosition = zoneLocalPos;

            if (_boxCollider == null) _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.center = zoneCenter;
            _boxCollider.size = zoneSize;

            Endpoints = new TransitionEndpointList(endpoints);

            UpdateEndpoints(renameEndpoint);
        }

        public void Load(TransitionZoneFieldValue fieldValue, bool renameEndpoint = false)
        {
            transform.localPosition = fieldValue.LocalPosition;

            if (_boxCollider == null) _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.center = fieldValue.ZoneCenter;
            _boxCollider.size = fieldValue.ZoneSize;

            Endpoints = new TransitionEndpointList(fieldValue.EndpointList.list);

            UpdateEndpoints(renameEndpoint);
        }
        #endregion

        #region DistanceCalculation
        private int GetEnteringPointIndex(Vector3 position)
        {
            int minDistanceIndex = -1;
            float minDistance = float.MaxValue;
            for (int i = 0; i < transform.childCount; i++)
            {
                float currDistance = GeometryCalcu.GetDistance3D(position, transform.GetChild(i).position);
                if (currDistance < minDistance)
                {
                    minDistance = currDistance;
                    minDistanceIndex = i;
                }
            }
            return minDistanceIndex;
        }
        #endregion
    }
}