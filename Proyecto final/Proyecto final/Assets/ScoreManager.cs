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
  // public TextMeshProUGUI HighScoreText;
  int Score = 0;
  // int HighScore = 0;

  private void Awake() {
    instance = this;
  }

  void Start() {
    PlayerPrefs.SetInt("score", 0);
    UsernameText.text = PlayerPrefs.GetString("username", "Anonymous");
    ScoreText.text = Score.ToString();
  }

  public void UpdateScore(int Points) {
    Score += Points;
    ScoreText.text = Score.ToString();
    PlayerPrefs.SetInt("score", Score);
  }
}
