using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;
    [SerializeField] private Transform LevelPartStart;
    [SerializeField] private List<Transform> Items;
    public List<float> ItemProbabilities;
    [SerializeField] private List<Transform> RoofItems;
    public List<float> RoofItemProbabilities;
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
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        float deltaX = (lastEndPosition.x - lastLevelPartTransform.position.x) / 2.0f;
        SpawnItem(Items, ItemProbabilities, lastLevelPartTransform.position + new Vector3(deltaX, 1.0f));
        SpawnItem(RoofItems, RoofItemProbabilities, lastLevelPartTransform.position + new Vector3(deltaX, 6.0f + 1.0f));
    }

    void SpawnItem(List<Transform> items, List<float> itemProbabilities, Vector3 position) {
        // 50% probabilty of spawning an object
        float randomValue = (float)Random.Range(-100f, 100f) / 100f;

        if (randomValue >= 0f) {
            int index = 0;
            float accumulatedProbability = 0f;

            for (; index < itemProbabilities.Count; index++) {
                accumulatedProbability += itemProbabilities[index];

                if (accumulatedProbability >= randomValue) {
                    break;
                }
            }

            if (index < itemProbabilities.Count) {
                Transform chosenItem = items[index];
                SpawnLevelPart(chosenItem, position);
            }
        }

    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition) {
        Instantiate(RoofPart, spawnPosition + new Vector3(0f, 12.5f), Quaternion.identity);
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
