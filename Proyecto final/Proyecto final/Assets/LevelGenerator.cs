using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;
    [SerializeField] private Transform LevelPartStart;
    [SerializeField] private List<Transform> Items;
    [SerializeField] private List<Transform> LevelParts;
    [SerializeField] private Transform RoofPart;
    [SerializeField] private Player player;
    private Vector3 lastEndPosition;

    private void Awake() {
        lastEndPosition = LevelPartStart.Find("EndPosition").position;
    }

    void Update() {
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART) {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart() {
        Transform chosenLevelPart = LevelParts[Random.Range(0, LevelParts.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        int lim = 1 << Items.Count;
        int randomValue = Random.Range(-lim, +lim);

        if (randomValue >= 1) {
            int index = (int)System.Math.Log(randomValue, 2);
            Transform chosenItem = Items[index];
            float deltaX = (lastLevelPartTransform.Find("EndPosition").position.x - lastEndPosition.x) / 2.0f;
            SpawnLevelPart(chosenItem, lastEndPosition + new Vector3(deltaX, 1.0f));
        }

        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition) {
        Instantiate(RoofPart, spawnPosition + new Vector3(0f, 12.5f), Quaternion.identity);
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
