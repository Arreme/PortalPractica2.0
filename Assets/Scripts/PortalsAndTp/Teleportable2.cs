using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable2 : MonoBehaviour
{
    public Vector3 previousOffsetFromPortal { get; set; }
    public virtual void Teleport(PortalCamera fromPortal, PortalCamera toPortal, Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }
}
