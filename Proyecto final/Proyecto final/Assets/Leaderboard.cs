using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour {
    public LootLockerLeaderboard leaderboard;

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
