using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndSceneScript : MonoBehaviour {
    public TextMeshProUGUI EndSceneScoreText;
    int Score = 0;

    void Start() {
        Score = PlayerPrefs.GetInt("score", 0);
        EndSceneScoreText.text = "Tu puntaje: " + Score.ToString();
    }
}
