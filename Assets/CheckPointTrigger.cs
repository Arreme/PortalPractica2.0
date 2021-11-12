using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField] private int _number;
    private void OnTriggerEnter(Collider other)
    {
        StData.Checkpoint = _number;
        GetComponent<BoxCollider>().enabled = false;
    }
}
