using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndSceneScript : MonoBehaviour {
    public TextMeshProUGUI EndSceneScoreText;
    public LootLockerLeaderboard leaderboard;
    int score = 0;

    public IEnumerator SubmitScore() {
        yield return new WaitUntil(() => leaderboard != null);
        yield return leaderboard.SubmitScoreRoutine(score);
    }

    void Start() {
        score = PlayerPrefs.GetInt("score", 0);
        EndSceneScoreText.text = "Tu puntaje: " + score.ToString();
        StartCoroutine(SubmitScore());
    }
}
