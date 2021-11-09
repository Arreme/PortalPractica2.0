using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ProcessInteractable();
        }
    }

    private void ProcessInteractable()
    {
        Ray r = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(r, out RaycastHit info, 1.0f, _layer))
        {
            if (info.transform.TryGetComponent(out ButtonEvent button))
            {
                if (button.pressButton())
                {
                    //PlayOkSound
                } else
                {
                    //Play Nanai Sound
                }
            }
        }
    }
}
