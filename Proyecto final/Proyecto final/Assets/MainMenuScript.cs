using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LootLocker.Requests;

public class MainMenuScript : MonoBehaviour {
    public void GoToMenu() {
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

    public void Leaderboard()
    {
        SceneManager.LoadScene(4);
    }

    public void ReadUserInput(string input) {
        PlayerPrefs.SetString("username", input);

        LootLockerSDKManager.SetPlayerName(input, (response) => {
            if (response.success) {
                Debug.Log("Succesfully set player name");
            } else {
                Debug.Log("Could not set player name: " + response.Error);
            }
        });
    }
}
