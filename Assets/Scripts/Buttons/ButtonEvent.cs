using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private bool onceTrigger;
    private bool isActive = true;
    [SerializeField] private UnityEvent buttonEvent;

    public bool pressButton()
    {
        bool canBeCalled = isActive;
        if (canBeCalled)
            buttonEvent.Invoke();
        if (onceTrigger) isActive = false;
        return canBeCalled;
    }
}
