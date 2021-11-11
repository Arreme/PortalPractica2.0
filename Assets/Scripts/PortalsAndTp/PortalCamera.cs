using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public PortalCamera _linkedPortal;
    [SerializeField] private Camera _playerCamera;
    public Transform virtualPortal;
    private Camera _portalCamera;
    public MeshRenderer _screen;
    private RenderTexture _renderText;

    [SerializeField] private float _nearClipOffset;
    [SerializeField] private float _nearClipLimit;

    List<Teleportable2> _trackedTravellers;

    private void Awake()
    {
        _trackedTravellers = new List<Teleportable2>();
        _portalCamera = transform.GetComponentInChildren<Camera>();
        _portalCamera.enabled = false;
    }    

    private void CreateViewTexture() 
    {
        if (_renderText == null || _renderText.width != Screen.width || _renderText.height != Screen.height)
        {
            if (_renderText != null)
            {
                _renderText.Release();
            }
            _renderText = new RenderTexture(Screen.width, Screen.height, 0);
            _portalCamera.targetTexture = _renderText;
            _linkedPortal._screen.material.SetTexture("_MainTex", _renderText);
        }
    }

    public void Render()
    {
        if (!VisibleFromCamera(_linkedPortal._screen, _playerCamera))
        {
            return;
        }
        _screen.enabled = false;
        CreateViewTexture();

        var m = transform.localToWorldMatrix * virtualPortal.transform.worldToLocalMatrix * _playerCamera.transform.localToWorldMatrix;
        _portalCamera.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        _linkedPortal._screen.material.SetInt("displayMask", 0);

        SetNearClipPlane();
        _portalCamera.Render();

        _linkedPortal._screen.material.SetInt("displayMask",1);

        _screen.enabled = true;
    }

    void SetNearClipPlane()
    {
        Transform clipPlane = transform;
        int dot = System.Math.Sign(Vector3.Dot(clipPlane.forward, transform.position - _portalCamera.transform.position));

        Vector3 camSpacePos = _portalCamera.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 camSpaceNormal = _portalCamera.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float camSpaceDst = -Vector3.Dot(camSpacePos, camSpaceNormal) + _nearClipOffset;

        if (Mathf.Abs(camSpaceDst) > _nearClipLimit)
        {
            Vector4 clipPlaneCameraSpace = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDst);
            _portalCamera.projectionMatrix = _playerCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        }
        else
        {
            _portalCamera.projectionMatrix = _playerCamera.projectionMatrix;
        }
    }

    public static bool VisibleFromCamera(Renderer renderer, Camera camera)
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
    }

    void LateUpdate()
    {
        HandleTravellers();
    }

    void HandleTravellers()
    {

        for (int i = 0; i < _trackedTravellers.Count; i++)
        {
            Teleportable2 traveller = _trackedTravellers[i];
            Transform travellerT = traveller.transform;
            Vector3 offsetFromPortal = travellerT.position - transform.position;
            int portalSide = System.Math.Sign(Vector3.Dot(offsetFromPortal, transform.forward));
            int portalSideOld = System.Math.Sign(Vector3.Dot(traveller.previousOffsetFromPortal, transform.forward));
            traveller.OffsetCollider(this);
            if (portalSide != portalSideOld)
            {
                var m = _linkedPortal.transform.localToWorldMatrix * _linkedPortal.virtualPortal.worldToLocalMatrix * traveller.transform.localToWorldMatrix;
                traveller.Teleport(this, _linkedPortal, m.GetColumn(3), m.rotation);
                _linkedPortal.OnTravellerEnterPortal(traveller);
                _trackedTravellers.RemoveAt(i);
                i--;
            }
            else
            {
                traveller.previousOffsetFromPortal = offsetFromPortal;
            }
        }
    }

    public void OnTravellerEnterPortal(Teleportable2 traveller)
    {
        if (!_trackedTravellers.Contains(traveller))
        {
            traveller.previousOffsetFromPortal = traveller.transform.position - transform.position;
            _trackedTravellers.Add(traveller);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var traveller = other.GetComponent<Teleportable2>();
        if (traveller)
        {
            OnTravellerEnterPortal(traveller);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var traveller = other.GetComponent<Teleportable2>();
        if (traveller && _trackedTravellers.Contains(traveller))
        {
            _trackedTravellers.Remove(traveller);
        }
    }
}
