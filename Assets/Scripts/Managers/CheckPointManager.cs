using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private CheckPointTrigger[] _checkPoints;
    private void Start()
    {
        GameObject checkPointFather = GameObject.Find("CheckPoints");
        _checkPoints = checkPointFather.GetComponentsInChildren<CheckPointTrigger>();
        _player.SetPositionAndRotation(_checkPoints[StData.Checkpoint].transform.position,_checkPoints[StData.Checkpoint].transform.rotation);
        Physics.SyncTransforms();
    }

    public void GoToCheckPoint()
    {
        _player.position = _checkPoints[StData.Checkpoint].transform.position;
        _player.GetComponent<FPSController>().RecalculateDirection(_checkPoints[StData.Checkpoint].transform.rotation);
        Physics.SyncTransforms();
        
    }
}
