using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScriptV2 : MonoBehaviour
{
    enum Lanes
    {
        Lane1 = 0,
        Lane2 = 1,
        Lane3 = 2,
        Lane4 = 3,
        Lane5 = 4

    }

    [SerializeField]
    DifficultyTier[] tiers;
    DifficultyTier wave;
    float spawnTime;
    int difficultyIndex;

    void Start()
    {
        difficultyIndex = -1;
        ProgressNextTier();
    }

    private void Update()
    {
        if (spawnTime > wave.spawnRate)
        {

        }
        else
        {
            spawnTime = 0;
        }
    }

    private void Spawn()
    {

    }

    void ProgressNextTier()
    {
        spawnTime = 0;
        difficultyIndex++;
        wave = tiers[difficultyIndex];
    }

    [System.Serializable]
    class DifficultyTier
    {
        public string name;
        public float hordeChance;
        public float spawnRate;
        public List<LanesSelector> lanes;
        public List<HordePrefab> horde;
        public List<EnemyEntity> enemies;
    }

    [System.Serializable]
    class HordePrefab
    {
        public string name;
        public GameObject hordePrefab;
        public float breathingTime;
    }

    [System.Serializable]
    class EnemyEntity
    {
        public GameObject enemyPrefab;
        public float weight;
    }

    [System.Serializable]
    class LanesSelector
    {
        public Lanes lane;
        public float weight;

    }
}
