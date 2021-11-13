using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void GameOverEvent()
    {
        GetComponent<CheckPointManager>().GoToCheckPoint();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("FinalScene2"); 
    }

    public void ExitButton()
    {
        
    }
}
