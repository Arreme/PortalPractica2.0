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
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = toPortal.transform.TransformDirection(toPortal.virtualPortal.InverseTransformDirection(rb.velocity));
            transform.localScale *= toPortal.transform.localScale.x / fromPortal.transform.localScale.x;
        }
        Physics.SyncTransforms();
    }

    public virtual void OffsetCollider(PortalCamera myPortal)
    {
        
        
    }
}
