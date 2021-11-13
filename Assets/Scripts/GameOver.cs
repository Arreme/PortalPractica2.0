using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void RestartButton()
    {
        SceneManager.LoadScene("FinalScene"); 
    }

    public void ExitButton()
    {
        
    }
}
