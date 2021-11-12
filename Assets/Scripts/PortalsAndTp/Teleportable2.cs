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
        if(TryGetComponent(out BoxCollider col))
        {
            Vector3 point = myPortal.transform.TransformPoint(new Vector3(0, 0, 0.3f));
            float t = (myPortal.transform.forward.x * (point.x - transform.position.x)) +
                                                 (myPortal.transform.forward.y * (point.y - transform.position.y)) +
                                                 (myPortal.transform.forward.z * (point.z - transform.position.z));

            Vector3 colPoint = new Vector3(transform.position.x + (t * myPortal.transform.forward.x),
                                  transform.position.y + (t * myPortal.transform.forward.y),
                                  transform.position.z + (t * myPortal.transform.forward.z));

            col.center = transform.InverseTransformPoint(colPoint);
            col.size = new Vector3(0.2f, 0.2f, 0.2f);
            if (col.center.magnitude >= 0.5)
            {
                col.center = Vector3.zero;
                col.size = new Vector3(1f, 1f, 1f);
            }
        }
        
        
        

    }
}
