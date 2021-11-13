using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private bool _isOpening = false;
    [SerializeField] private float _secondsToMove;
    private float _currentState = 0;

    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _destination;


    public void Open()
    {
        _isOpening = true;  
    }

    public void Close()
    {
        _isOpening = false; 
    }


    private void Update()
    {
         _currentState += (Time.deltaTime / _secondsToMove) * (_isOpening ? 1 : -1);
         _currentState = Mathf.Clamp(_currentState, 0, 1); 
         transform.position = Vector3.Lerp(_origin.position, _destination.position, _currentState);
        
    }
}
