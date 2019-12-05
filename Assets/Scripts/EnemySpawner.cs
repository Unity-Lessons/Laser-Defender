using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig myCurrentWave)
    {
        for (int enemyCount = 0; enemyCount < myCurrentWave.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                myCurrentWave.GetEnemyPrefab(),
                myCurrentWave.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(myCurrentWave);
            yield return new WaitForSeconds(myCurrentWave.GetTimeBtwnSpawns() * Random.Range(0.1f, myCurrentWave.GetSpawnRandFactor()));
        }
    }
}
