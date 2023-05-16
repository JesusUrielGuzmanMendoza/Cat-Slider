using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LootLockerLeaderboard : MonoBehaviour {
    string leaderboardKey = "globalHighscore";
    [SerializeField] private List<TextMeshProUGUI> Names;
    [SerializeField] private List<TextMeshProUGUI> Scores;

    public IEnumerator SubmitScoreRoutine(int scoreToUpload) {
        bool done = false;

        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardKey, (response) => {
            if (response.success) {
                Debug.Log("Succesfully uploaded score");
                done = true;
            } else {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }

            StartCoroutine(FetchTopHighscoresRoutine());
        });

        yield return new WaitWhile(() => done == false);
    }

    IEnumerator FetchTopHighscoresRoutine() {
        bool done = false;

        LootLockerSDKManager.GetScoreList(leaderboardKey, 10, 0, (response) => {
            if (response.success) {
                LootLockerLeaderboardMember [] members = response.items;
                int loopLength = (members.Length < Names.Count ? members.Length : Names.Count);

                for (int i = 0; i < loopLength; i++) {
                    Names[i].text = (i + 1).ToString() + ". " + (members[i].player.name == "" ? members[i].player.id : members[i].player.name);
                    Scores[i].text = members[i].score.ToString();
                }

                done = true;
            } else {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }
        });

        yield return new WaitWhile(() => !done);
    }

    void Start() {
        StartCoroutine(FetchTopHighscoresRoutine());
    }
}
