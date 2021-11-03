using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    [SerializeField] private float _teleportOffset;
    private CharacterController _controller;

    private bool _teleporing;
    Vector3 _dir;
    Vector3 _pos;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PortalCamera portal))
        {
            _teleporing = true;

            Vector3 l_pos = portal._linkedPortal.virtualPortal.InverseTransformPoint(transform.position);
            Vector3 l_dir = portal._linkedPortal.virtualPortal.InverseTransformDirection(transform.forward);

            _dir = portal._linkedPortal.transform.TransformDirection(l_dir);
            _pos = portal._linkedPortal.transform.TransformPoint(l_pos) + (portal._linkedPortal.transform.forward * _teleportOffset);
        }
    }

    private void LateUpdate()
    {
        if (_teleporing)
        {
            _teleporing = false;
            _controller.enabled = false;
            transform.position = _pos;
            transform.forward = _dir;
            if (TryGetComponent(out FPSController fpsController))
            {
                fpsController.recalculateYawAndPitch();
            }
            _controller.enabled = true;
        }
    }

}
