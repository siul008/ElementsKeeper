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

    enum WaveState
    {
        PROGRESS,
        SPAWNING,
        WAITINGBIGWAVE,
        BIGWAVE
    }

    [SerializeField] LightController playerLight;
    [SerializeField] DifficultyTier[] tiers;
    [SerializeField] Transform[] lanesTransform;
    DifficultyTier wave;
    float spawnTime;
    int difficultyIndex;
    WaveState state;
    bool lightCalled;
    [SerializeField] float tierDuration;
    [SerializeField] float minMultSpawnRate;
    float lightDuration;
    float tierTime;
       
    void Start()
    {
        tierTime = 0;
        difficultyIndex = -1;
        ProgressNextTier();
        state = WaveState.PROGRESS;
        lightCalled = false;
        lightDuration = playerLight.GetTurnOffDuration();
    }

    private void Update()
    {
        if (spawnTime >= wave.spawnRate * GetCurrentSpawnRateMult() && state == WaveState.PROGRESS)
        {
            state = WaveState.SPAWNING;
            ChooseSpawn();
        }
        else
        {
            spawnTime += Time.deltaTime;
        }
        if (tierTime >= tierDuration - lightDuration)
        {
            playerLight.TurnOffLight();
            lightCalled = true;
        }
        if (tierTime >= tierDuration && state == WaveState.PROGRESS)
        {
            state = WaveState.WAITINGBIGWAVE;
        }
        else
        {
            tierTime += Time.deltaTime;
        }
        if (state == WaveState.WAITINGBIGWAVE && NoEnemyLeft())
        {
            StartBigWave();
        }
        if (state == WaveState.BIGWAVE && NoEnemyLeft())
        {
            playerLight.TurnOnLight();
            ProgressNextTier();
        }

    }

    bool NoEnemyLeft()
    {
        return (GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
    }

    private void ChooseSpawn()
    {
        //Is a horde spawn
        if (Random.Range(0, 101) <= wave.hordeChance)
        {
            DifficultyTier tier;
            //Is a horde from the tier bellow
            if (difficultyIndex > 0 && Random.Range(0, 101) <= wave.bellowTierHordeChance)
            {
                tier = tiers[difficultyIndex - 1];
            }
            //Is a horde from the current wave
            else
            {
                tier = wave;
            }
            HordePrefab randomHorde = tier.horde[Random.Range(0, tier.horde.Count)];
            Transform randomLane = GetRandomLane(tier);

            Spawn(randomLane, randomHorde.hordePrefab);
            spawnTime = 0 - randomHorde.breathingTime;
        }
        //Is a basic enemy spawn
        else
        {
            Transform randomLane = GetRandomLane(wave);
            GameObject randomEnemy = GetRandomEnemy(wave);

            Spawn(randomLane, randomEnemy);
            spawnTime = 0;
        }
    }

    float GetCurrentSpawnRateMult()
    {
        return (1 - (tierTime * (1 - minMultSpawnRate) / tierDuration));
    }

    void Spawn(Transform lanePos, GameObject enemy)
    {
        Instantiate(enemy, lanePos.position, Quaternion.identity);
        state = WaveState.PROGRESS;
    }

    GameObject GetRandomEnemy(DifficultyTier tier)
    {
        int totalWeight = 0;

        foreach (EnemyEntity enemy in tier.enemies)
        {
            totalWeight += enemy.weight;
        }
        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0;
        foreach (EnemyEntity enemy in tier.enemies)
        {
            cumulativeWeight += enemy.weight;
            if (randomValue <= cumulativeWeight)
            {
                return enemy.enemyPrefab;
            }
        }
        Debug.LogError("No enemy returned");
        return null;
    }

    Transform GetRandomLane(DifficultyTier tier)
    {
        int totalWeight = 0;

        foreach (LanesSelector lane in tier.lanes)
        {
            totalWeight += lane.weight;
        }
        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0;
        foreach (LanesSelector lane in tier.lanes)
        {
            cumulativeWeight += lane.weight;
            if (randomValue <= cumulativeWeight)
            {
                return lanesTransform[(int)lane.lane];
            }
        }
        Debug.LogError("No lanes returned");
        return lanesTransform[0];
    }

    void StartBigWave()
    {
        Instantiate(wave.endWave, lanesTransform[0].position, Quaternion.identity);
        state = WaveState.BIGWAVE;
    }

    void ProgressNextTier()
    {
        tierTime = 0;
        if (spawnTime > 0)
        {
            spawnTime = 0;
        }
        if (difficultyIndex + 1 < tiers.Length)
        {
            difficultyIndex++;
        }
        wave = tiers[difficultyIndex];
        lightCalled = true;
    }

    [System.Serializable]
    class DifficultyTier
    {
        public string name;
        public float hordeChance;
        public float bellowTierHordeChance;
        public float spawnRate;
        public GameObject endWave;
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
        public int weight;
    }

    [System.Serializable]
    class LanesSelector
    {
        public Lanes lane;
        public int weight;

    }
}
