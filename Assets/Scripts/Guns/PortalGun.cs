using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public string _portalEnabledTag;
    [SerializeField] private GameObject _previewPortal;
    [SerializeField] private float _offset;
    public Camera _playerCamera;
    public float _maxDistance;
    public LayerMask _portalLayerMask;

    [SerializeField] private GameObject _bluePortal;
    [SerializeField] private GameObject _orangePortal;

    private bool _isActive;
    [SerializeField] private PortalUIHandler _uiHandler;
    private float _currScale = 1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            _isActive = movePreviewPortal();
            float _mouseDelta = Input.mouseScrollDelta.y;
            if (_mouseDelta != 0)
            {
                _currScale += _mouseDelta * 0.05f;
                _previewPortal.transform.localScale = new Vector3(_currScale,_currScale,1);
            }
        }
        _previewPortal.SetActive(_isActive);
        if (Input.GetMouseButtonUp(0) && _isActive)
        {
            _bluePortal.gameObject.SetActive(true);
            _bluePortal.transform.SetPositionAndRotation(_previewPortal.transform.position + (_previewPortal.transform.forward * _offset), _previewPortal.transform.rotation);
            _bluePortal.transform.localScale = _previewPortal.transform.localScale;
            if ((_orangePortal.transform.position - _bluePortal.transform.position).magnitude <= 2.5)
            {
                _orangePortal.gameObject.SetActive(false);
            }
            _isActive = false;
            _uiHandler.updatingUI();
            AudioManager._instance.PlayAudio((int)Audios.LASER1);
        }
        if (Input.GetMouseButtonUp(1) && _isActive) 
        {
            _orangePortal.gameObject.SetActive(true);
            _orangePortal.transform.SetPositionAndRotation(_previewPortal.transform.position + (_previewPortal.transform.forward * _offset), _previewPortal.transform.rotation);
            _orangePortal.transform.localScale = _previewPortal.transform.localScale;
            if ((_bluePortal.transform.position - _orangePortal.transform.position).magnitude <= 2.1)
            {
                _bluePortal.gameObject.SetActive(false);
            }
            _isActive = false;
            _uiHandler.updatingUI();
            AudioManager._instance.PlayAudio((int)Audios.LASER1);
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
