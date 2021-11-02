using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PreviewPortal : MonoBehaviour
{
    [SerializeField] private List<Transform> _controlPoints;
    [SerializeField] private PortalGun _rayCastInfo;

    [SerializeField] private float limit;
    public bool isValidPosition()
    {
        return _controlPoints.All(x => checkPoint(x));
    }

    private bool checkPoint(Transform point)
    {
        Ray r = _rayCastInfo._playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(r, out RaycastHit hitInfo, _rayCastInfo._maxDistance + 20,_rayCastInfo._portalLayerMask))
        {
            Vector3 diff = point.position - hitInfo.point;
            float distance = diff.magnitude;
            Debug.Log(distance);
            if (distance >= limit) return false;
            if (!hitInfo.transform.CompareTag(_rayCastInfo._portalEnabledTag)) return false;
            //Forward
            return true;
        }
        return false;
    }
}
