using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public string _portalEnabledTag;
    [SerializeField] private GameObject _previewPortal;
    public Camera _playerCamera;
    public float _maxDistance;
    public LayerMask _portalLayerMask;

    [SerializeField] private GameObject _bluePortal;
    [SerializeField] private GameObject _orangePortal;

    private bool _isActive;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            _isActive = movePreviewPortal();
        }
        _previewPortal.SetActive(_isActive);
        if (Input.GetMouseButtonUp(0) && _isActive)
        {
            _bluePortal.transform.SetPositionAndRotation(_previewPortal.transform.position,_previewPortal.transform.rotation);
            _isActive = false;
        }
        if (Input.GetMouseButtonUp(1) && _isActive) 
        {
            _orangePortal.transform.SetPositionAndRotation(_previewPortal.transform.position, _previewPortal.transform.rotation);
            _isActive = false;
        }
    }

    bool movePreviewPortal()
    {
        Ray r = _playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0.0f));
        if (Physics.Raycast(r, out RaycastHit hitInfo, _maxDistance, _portalLayerMask))
        {
            if (hitInfo.transform.gameObject.CompareTag(_portalEnabledTag))
            {
                _previewPortal.transform.position = hitInfo.point;
                _previewPortal.transform.forward = hitInfo.normal;
                return _previewPortal.GetComponent<PreviewPortal>().isValidPosition();

            } else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }
}
