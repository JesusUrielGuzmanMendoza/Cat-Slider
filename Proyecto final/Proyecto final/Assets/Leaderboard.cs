using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour {
    [SerializeField] private List<TextMeshProUGUI> Names;
    [SerializeField] private List<TextMeshProUGUI> Scores;
    private string publicLeaderboardKey = "b584c123daffbd48c1280d2077a4b3416e31929adefca71cfcba89edfd56cd6c";

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }

    private void Start() {
        GetLeaderboard();
    }

    public void GetLeaderboard() {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((response) => {
            int loopLength = (response.Length < Names.Count ? response.Length : Names.Count);

            for (int i = 0; i < loopLength; i++) {
                Names[i].text = response[i].Username;
                Scores[i].text = response[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score) {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((_) => {
            GetLeaderboard();
        }));
    }
}
