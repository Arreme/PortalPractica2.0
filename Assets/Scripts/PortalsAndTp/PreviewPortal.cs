using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PreviewPortal : MonoBehaviour
{
    [SerializeField] private List<Transform> _controlPoints;
    [SerializeField] private PortalGun _rayCastInfo;

    [SerializeField] private float _limit;
    [SerializeField] private float _angleThreshold;
    public bool isValidPosition()
    {
        foreach (Transform point in _controlPoints)
        {
            if (Physics.Raycast(_rayCastInfo._playerCamera.transform.position, point.transform.position - _rayCastInfo._playerCamera.transform.position, out RaycastHit hitInfo, float.MaxValue, _rayCastInfo._portalLayerMask))
            {
                if (!hitInfo.transform.gameObject.CompareTag(_rayCastInfo._portalEnabledTag))
                    return false;


                if (Vector3.Distance(hitInfo.point, point.transform.position) > _limit)
                    return false;


                if (Vector3.Angle(hitInfo.normal, point.transform.forward) > _angleThreshold)
                    return false;
            }
            else return false;
        }
        return true;
    }

}
