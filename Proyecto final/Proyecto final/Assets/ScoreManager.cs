using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LootLocker.Requests;

public class ScoreManager : MonoBehaviour {
  public static ScoreManager instance;
  public TextMeshProUGUI UsernameText;
  public TextMeshProUGUI ScoreText;
  int Score = 0;

  private void Awake() {
    instance = this;
  }

  void Start() {
    PlayerPrefs.SetInt("score", 0);
    string username = PlayerPrefs.GetString("username", "Anonymous");
    UsernameText.text = username;

    if (username == "Anonymous") {
      LootLockerSDKManager.SetPlayerName("Anonymous", (response) => {
          if (response.success) {
              Debug.Log("Succesfully reset player name");
          } else {
              Debug.Log("Could not reset player name: " + response.Error);
          }
      });
    }


    ScoreText.text = Score.ToString();
  }

  public void UpdateScore(int Points) {
    Score += Points;
    ScoreText.text = Score.ToString();
    PlayerPrefs.SetInt("score", Score);
  }
}
