using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _companion;
    public void Spawn()
    {
        Instantiate(_companion, _spawnPoint.position, _spawnPoint.rotation);
    }
}
