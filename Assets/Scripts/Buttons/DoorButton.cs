using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{

    [SerializeField] private Door _linkedDoor;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
            _linkedDoor.Open(); 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
            _linkedDoor.Close();
    }

}
