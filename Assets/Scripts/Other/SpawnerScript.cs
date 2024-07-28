using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] Transform[] spawns;
    [SerializeField] List<Wave> waves = new List<Wave>();
    int index;
    float time;
    float timeToWait;

    public static SpawnerScript Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        index = 0;
        timeToWait = waves[index].delay;
        time = 0;
    }

    private void Update()
    {
        if (time >= timeToWait)
        {
            time = 0;
            Spawn();
        }
        else
        {
            time += Time.deltaTime;
        }
    }

    public void EnemyDied()
    {
        /*remaining--;
        if (remaining <= 0 && enemiesToSpawn <= 0)
        {
            StartCoroutine(NextWave());
        }*/
    }


    [System.Serializable]
    public class Wave 
    {
        public string name;
        public int lane;
        public GameObject enemy;
        public float delay;
    }
}
