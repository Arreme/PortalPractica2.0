using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretAngleDetector : MonoBehaviour
{
    [SerializeField] private float maxAngle;
    private bool isActive = true;

    [SerializeField] private TurretLaser _turretLaser;

    private void Update()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);
        if (isActive && angle > maxAngle)
        {
            isActive = false;
            _turretLaser.updateState(false);
            AudioManager._instance.PlayAudio((int)Audios.OBJBREAK);
        }
        if (!isActive & angle < maxAngle)
        {
            isActive = true;
            _turretLaser.updateState(true);
            AudioManager._instance.PlayAudio((int) Audios.LASER2);
        }
    }
}
