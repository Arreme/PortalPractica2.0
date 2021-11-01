using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private PortalCamera[] _portals;
    void Awake()
    {
        _portals = FindObjectsOfType<PortalCamera>();
        
    }

    void OnPreCull()
    {
        foreach (PortalCamera cam in _portals)
        {
            cam.Render();
        }
    }
}
