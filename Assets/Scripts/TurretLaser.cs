using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
    [SerializeField] private LineRenderer laserRenderer;
    [SerializeField] private LayerMask laserLayerMask;
    [SerializeField] bool isActive = true;
    [SerializeField] float maxLaserDist; 

    void updateState(bool isActive)
    {
        laserRenderer.enabled = isActive;
        this.isActive = isActive; 
    }


    private void Update()
    {
        if(isActive)
        {
        
            Ray r = new Ray(laserRenderer.transform.position, laserRenderer.transform.forward);

            if(Physics.Raycast(r, out RaycastHit hitInfo, maxLaserDist, laserLayerMask))
            {
                laserRenderer.SetPosition(1, Vector3.forward * hitInfo.distance); //laserRenderer 2nd point to hit distance 
               
                /*
                if (hitInfo.transform.gameObject.TryGetComponent(out HealthSystem hs))
                {
                    hs.takeDamage(hs.maxHealth); 
                }
                */ 

                //si el objeto contra el que toca tiene un laser, lo activa
                if (hitInfo.transform.gameObject.TryGetComponent(out TurretLaser laser)) {
                    laser.updateState(true); 
                }

            }
               
            
            else
                laserRenderer.SetPosition(1, Vector3.forward * maxLaserDist); //laserRenderer 2nd point to "infinite" distance 
            

        }
    }
}
