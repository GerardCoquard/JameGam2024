using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Wave", menuName = "Wave/WaveData")]
public class WaveData : ScriptableObject
{
    public List<EnemyData> gate1Enemies;
    public List<EnemyData> gate2Enemies;
    public List<EnemyData> gate3Enemies;
    public List<EnemyData> gate4Enemies;
    public List<EnemyData> gate5Enemies;
}
