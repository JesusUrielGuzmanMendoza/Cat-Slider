using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;
    [SerializeField] private Transform LevelPartStart;
    [SerializeField] private List<Transform> LevelParts;
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
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition) {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
