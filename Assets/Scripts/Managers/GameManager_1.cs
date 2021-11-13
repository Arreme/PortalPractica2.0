using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_1 : MonoBehaviour
{
    public static GameManager_1 _gameManager = null;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _ui;


    void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = this;
          
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void DeathEvent()
    {
        _ui.SetActive(true);
        _player.GetComponent<FPSController>().enabled = false;
        _player.GetComponent<CharacterController>().enabled = false;
    }


    
}
