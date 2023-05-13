using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class EndSceneScript : MonoBehaviour {
    public TextMeshProUGUI EndSceneScoreText;
    public UnityEvent<string, int> submitScoreEvent;
    int Score = 0;

    public void SubmitScore() {
        submitScoreEvent.Invoke(PlayerPrefs.GetString("username", "Anonymous"), Score);
    }

    void Start() {
        Score = PlayerPrefs.GetInt("score", 0);
        EndSceneScoreText.text = "Tu puntaje: " + Score.ToString();
        SubmitScore();
    }
}
