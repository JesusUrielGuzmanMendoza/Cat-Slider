using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame() {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene(3);
    }

    public void ReadUserInput(string input) {
        PlayerPrefs.SetString("username", input);
        Debug.Log(input);
    }
}
