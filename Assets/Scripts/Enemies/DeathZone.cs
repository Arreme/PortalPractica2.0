using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager_1._gameManager.DeathEvent();

        else if (other.CompareTag("Cube"))
            Destroy(other.gameObject);

        else if (other.CompareTag("Turret"))
            Destroy(other.gameObject);
    }

    
}
