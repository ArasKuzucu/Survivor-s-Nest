using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveSpawner : MonoBehaviour
{
    //Wave info for player
    public TextMeshProUGUI wavetxt;
    //States of the waves
    public enum SpawnState
    {
        SPAWNING, WAITING, COUNTING
    };
   
    [System.Serializable]
    public class Wave
    {
        [Header("How many and how types of enemy you want: ")]
        public string name;       
        public float rate;
        public Transform[] enemies; 

    }
    

    public Wave[] waves;

    [HideInInspector]
    public int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;
    private Experience exp;
    private Shooting st;
    public Transform spawnPoint;

    private SpawnState state = SpawnState.COUNTING;
    private void Start()
    {
        //These values for constructor to Playerdata script
        exp = FindObjectOfType<Experience>();
        st = FindObjectOfType<Shooting>();
        PlayerData data = SaveSystem.LoadPlayerLevel();

        if (data == null)
        {
            wavetxt.text = "Wave " + nextWave;
            nextWave = 0;
        }
        else
        {
            wavetxt.text = "Wave " + data.level;
            nextWave = data.level;
        }
       
        waveCountdown = timeBetweenWaves;
        
        
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (EnemyIsAlive() == false)
            {
                //Begin  a new round
                Debug.Log("Wave Completed");


                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE LOOPİNG...");
        }
        else
        {
            nextWave++;
            wavetxt.text = "Wave " + nextWave;
            SaveSystem.SavePlayerProgress(exp,this, st);
        }

    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Target") == null)
            {
                return false;
            }
        }

        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.enemies.Length; i++)
        {
            SpawnEnemy(_wave.enemies[i]);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {

        Debug.Log("Spawning Enemy: " + _enemy.name);
        Transform _sp = spawnPoint;
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
