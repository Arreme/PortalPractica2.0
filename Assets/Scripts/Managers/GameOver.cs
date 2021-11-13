using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) GameOverEvent();
    }

    public void GameOverEvent()
    {
        GetComponent<CheckPointManager>().GoToCheckPoint();
    }
}
