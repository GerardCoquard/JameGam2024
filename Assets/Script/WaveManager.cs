using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class WaveManager : MonoBehaviour
{
    public PathInfo gate1;
    public PathInfo gate2;
    public PathInfo gate3;
    public PathInfo gate4;
    public PathInfo gate5;

    public List<WaveData> waves;

    int currentwaveIndex = 0;

    public void SpawnWave()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        for (int i = 0; i < waves[currentwaveIndex].gate1Enemies.Count; i++)
        {
            GameObject enemy = GameObject.Instantiate(waves[currentwaveIndex].gate1Enemies[i].EnemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.GetComponent<MoveEnemie>().pathInfo = gate1;
            yield return new WaitForSeconds(GameManager.gameData.spawnTimeBetweenEnemys);
        }
        for (int i = 0; i < waves[currentwaveIndex].gate2Enemies.Count; i++)
        {
            GameObject enemy = GameObject.Instantiate(waves[currentwaveIndex].gate2Enemies[i].EnemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.GetComponent<MoveEnemie>().pathInfo = gate2;
            yield return new WaitForSeconds(GameManager.gameData.spawnTimeBetweenEnemys);
        }
        for (int i = 0; i < waves[currentwaveIndex].gate3Enemies.Count; i++)
        {
            GameObject enemy = GameObject.Instantiate(waves[currentwaveIndex].gate3Enemies[i].EnemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.GetComponent<MoveEnemie>().pathInfo = gate3;
            yield return new WaitForSeconds(GameManager.gameData.spawnTimeBetweenEnemys);
        }
        for (int i = 0; i < waves[currentwaveIndex].gate4Enemies.Count; i++)
        {
            GameObject enemy = GameObject.Instantiate(waves[currentwaveIndex].gate4Enemies[i].EnemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.GetComponent<MoveEnemie>().pathInfo = gate4;
            yield return new WaitForSeconds(GameManager.gameData.spawnTimeBetweenEnemys);
        }
        for (int i = 0; i < waves[currentwaveIndex].gate5Enemies.Count; i++)
        {
            GameObject enemy = GameObject.Instantiate(waves[currentwaveIndex].gate5Enemies[i].EnemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.GetComponent<MoveEnemie>().pathInfo = gate5;
            yield return new WaitForSeconds(GameManager.gameData.spawnTimeBetweenEnemys);
        }
        currentwaveIndex++;
    }
}
