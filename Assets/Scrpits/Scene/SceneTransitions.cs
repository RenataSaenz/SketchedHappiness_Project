using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public void ChangeScene(string levelInt)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelInt);
    }
    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }

}
