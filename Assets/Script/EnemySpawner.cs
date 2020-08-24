using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour{

    [SerializeField] List<WaveConfig> waveConfigs;
    int waveIndex = 0;

    void Start()
    {
        InvokeRepeating("startSpawn", 1f, 30f);
    }

    private void startSpawn()
    {
        for (int i = waveIndex; i < waveConfigs.Capacity; i++)
        {
            var currentWave = waveConfigs[i];
            StartCoroutine(SpawnWaveEnemies(currentWave));
        }
    }
    private IEnumerator SpawnWaveEnemies(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++) {
            Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].position,
                Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
     }











}