using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public static Action OnWaveEnd;
    
    public PathInfo gate1;
    public PathInfo gate2;
    public PathInfo gate3;
    public PathInfo gate4;
    public PathInfo gate5;
    
    private List<EnemyData> gate1Enemies = new List<EnemyData>();
    private List<EnemyData> gate2Enemies = new List<EnemyData>();
    private List<EnemyData> gate3Enemies = new List<EnemyData>();
    private List<EnemyData> gate4Enemies = new List<EnemyData>();
    private List<EnemyData> gate5Enemies = new List<EnemyData>();

    public List<WaveData> waves;
    public int loopFirstWaveIndex;
    [FormerlySerializedAs("levelsToIncreaseDifficulty")] public int wavesToIncreaseDifficulty;
    [SerializeField] GameObject _buttonNextRound;

    private int currentwaveIndex = 0;
    private int loops = 0;
    private bool spawning;

    private void Awake()
    {
        instance = this;
    }

    public void CheckIfWaveEnded()
    {
        if (spawning) return;
        StopAllCoroutines();
        StartCoroutine(CheckEnemiesAlive());
    }

    IEnumerator CheckEnemiesAlive()
    {
        yield return new WaitForSeconds(0.5f);
        if (FindObjectsOfType<Enemy>().Length <= 0)
        {
            OnWaveEnd?.Invoke();
            _buttonNextRound.SetActive(true);
        }
    }

    public void SpawnWave()
    {
        _buttonNextRound.SetActive(false);
        ClearEnemies();
        LoadEnemiesToGates(waves[currentwaveIndex]);
        for (int i = 0; i < loops; i++)
        {
            LoadEnemiesToGates(waves[waves.Count-1]);//Ultima wave o penultima?
        }
        CalculateNextWaveIndex();
        
        StopAllCoroutines();
        StartCoroutine(Spawn());
    }

    private void CalculateNextWaveIndex()
    {
        if (currentwaveIndex + 1 >= waves.Count)
        {
            loops++;
            currentwaveIndex = loopFirstWaveIndex;
        }
        else
            currentwaveIndex++;
    }

    private void ClearEnemies()
    {
        gate1Enemies = new List<EnemyData>();
        gate2Enemies = new List<EnemyData>();
        gate3Enemies = new List<EnemyData>();
        gate4Enemies = new List<EnemyData>();
        gate5Enemies = new List<EnemyData>();
    }
    
    private void LoadEnemiesToGates(WaveData wave)
    {
        foreach (EnemyData enemy in wave.gate1Enemies)
        {
            gate1Enemies.Add(enemy);
        }
        
        foreach (EnemyData enemy in wave.gate2Enemies)
        {
            gate2Enemies.Add(enemy);
        }
        
        foreach (EnemyData enemy in wave.gate3Enemies)
        {
            gate3Enemies.Add(enemy);
        }
        
        foreach (EnemyData enemy in wave.gate4Enemies)
        {
            gate4Enemies.Add(enemy);
        }
        
        foreach (EnemyData enemy in wave.gate5Enemies)
        {
            gate5Enemies.Add(enemy);
        }
    }
    IEnumerator Spawn()
    {
        spawning = true;
        int maxRounds = GetMaxRounds();
        int currentRound = 0;

        while (currentRound < maxRounds)
        {
            if (gate1Enemies.Count > currentRound)
            {
                GameObject enemy = Instantiate(gate1Enemies[currentRound].EnemyPrefab, gate1.splineContainer.Spline.ToArray()[0].Position, Quaternion.identity);
                InitializeEnemy(enemy.GetComponent<Enemy>(),gate1Enemies[currentRound]);
                enemy.GetComponent<MoveEnemie>().pathInfo = gate1;
            }
            if (gate2Enemies.Count > currentRound)
            {
                GameObject enemy = Instantiate(gate2Enemies[currentRound].EnemyPrefab, gate2.splineContainer.Spline.ToArray()[0].Position, Quaternion.identity);
                InitializeEnemy(enemy.GetComponent<Enemy>(),gate2Enemies[currentRound]);
                enemy.GetComponent<MoveEnemie>().pathInfo = gate2;
            }
            if (gate3Enemies.Count > currentRound)
            {
                GameObject enemy = Instantiate(gate3Enemies[currentRound].EnemyPrefab, gate3.splineContainer.Spline.ToArray()[0].Position, Quaternion.identity);
                InitializeEnemy(enemy.GetComponent<Enemy>(),gate3Enemies[currentRound]);
                enemy.GetComponent<MoveEnemie>().pathInfo = gate3;
            }
            if (gate4Enemies.Count > currentRound)
            {
                GameObject enemy = Instantiate(gate4Enemies[currentRound].EnemyPrefab, gate4.splineContainer.Spline.ToArray()[0].Position, Quaternion.identity);
                InitializeEnemy(enemy.GetComponent<Enemy>(),gate4Enemies[currentRound]);
                enemy.GetComponent<MoveEnemie>().pathInfo = gate4;
            }
            if (gate5Enemies.Count > currentRound)
            {
                GameObject enemy = Instantiate(gate5Enemies[currentRound].EnemyPrefab, gate5.splineContainer.Spline.ToArray()[0].Position, Quaternion.identity);
                InitializeEnemy(enemy.GetComponent<Enemy>(),gate5Enemies[currentRound]);
                enemy.GetComponent<MoveEnemie>().pathInfo = gate5;
            }
            
            currentRound++;
            yield return new WaitForSeconds(GameManager.gameData.spawnTimeBetweenEnemies);
        }

        spawning = false;
    }

    private int GetMaxRounds()
    {
        int max = gate1Enemies.Count;
        if (gate2Enemies.Count > max) max = gate2Enemies.Count;
        if (gate3Enemies.Count > max) max = gate3Enemies.Count;
        if (gate4Enemies.Count > max) max = gate4Enemies.Count;
        if (gate5Enemies.Count > max) max = gate5Enemies.Count;

        return max;
    }

    private void InitializeEnemy(Enemy enemy, EnemyData enemyData)
    {
        int health = (int)Mathf.Pow(GameManager.gameData.enemyStatsMultiplier, GetTotalWaves()+1) * enemyData.health;
        health /= GameManager.gameData.healthSegmentAmount;
        health *= GameManager.gameData.healthSegmentAmount;
        
        int armor = (int)Mathf.Pow(GameManager.gameData.enemyStatsMultiplier, GetTotalWaves()+1) * enemyData.armor;
        armor /= GameManager.gameData.healthSegmentAmount;
        armor *= GameManager.gameData.healthSegmentAmount;
        
        int magic = (int)Mathf.Pow(GameManager.gameData.enemyStatsMultiplier, GetTotalWaves()+1) * enemyData.magicArmor;
        magic /= GameManager.gameData.healthSegmentAmount;
        magic *= GameManager.gameData.healthSegmentAmount;
        
        int currency = (int)Mathf.Pow(GameManager.gameData.enemyStatsMultiplier, GetTotalWaves()+1) * enemyData.currency;
        
        enemy.InitializeEnemy(health,armor,magic,currency,enemyData);
    }

    public int GetCurrentWave()
    {
        return (currentwaveIndex + (loops * waves.Count));
    }
    
    public float GetTotalWaves()
    {
        return (currentwaveIndex + (loops * waves.Count)) / (float)wavesToIncreaseDifficulty;
    }
}
