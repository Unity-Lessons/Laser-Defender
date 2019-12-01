using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int numberOfEnemies = 5;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints() 
    {
        var waveWaypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints; 
    }

    public float GetTimeBtwnSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRandFactor() { return spawnRandomFactor; }

    public float GetMoveSpeed() { return moveSpeed; }

    public float GetNumberOfEnemies() { return numberOfEnemies; }

}
