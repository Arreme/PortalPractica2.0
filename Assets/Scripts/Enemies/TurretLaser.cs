using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
    [SerializeField] private LineRenderer laserRenderer;
    [SerializeField] private float maxLaserDist;
    [SerializeField] private LayerMask laserLayerMask;
    [SerializeField] private bool isActive;

    public void updateState(bool isActive)
    {
        laserRenderer.enabled = isActive;
        this.isActive = isActive;
    }

    private void Update()
    {
        if (isActive)
        {
            Ray r = new Ray(laserRenderer.transform.position, laserRenderer.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, maxLaserDist, laserLayerMask))
            {
                laserRenderer.SetPosition(1, Vector3.forward * hitInfo.distance);
                if (hitInfo.transform.gameObject.TryGetComponent(out TurretLaser laser))
                {
                    if (hitInfo.transform.CompareTag("Turret"))
                    {
                        AudioManager._instance.PlayAudio((int)Audios.BURNT);
                        Destroy(hitInfo.transform.gameObject);
                    } else
                    {
                        laser.updateState(true);
                    }
                    
                }
                if (hitInfo.transform.gameObject.TryGetComponent(out AbstractHealthSystem hitTarget))
                {
                    hitTarget.takeLaserDamage();
                }
            }
            else
            {
                laserRenderer.SetPosition(1, Vector3.forward * maxLaserDist);
            }
        }
    }
}
