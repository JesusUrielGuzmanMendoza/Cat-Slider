using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LoginManager : MonoBehaviour {
    void Start() {
        PlayerPrefs.SetString("username", "Anonymous");
        StartCoroutine(LoginRoutine());
    }

    IEnumerator LoginRoutine() {
        bool done = false;

        LootLockerSDKManager.StartGuestSession((response) => {
            if (response.success) {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            } else {
                Debug.Log("Could not start session");
                done = true;
            }

            LootLockerSDKManager.SetPlayerName("Anonymous", (response) => {
                if (response.success) {
                    Debug.Log("Succesfully reset player name");
                } else {
                    Debug.Log("Could not reset player name: " + response.Error);
                }
            });
        });

        yield return new WaitWhile(() => done == false);
    }

}
