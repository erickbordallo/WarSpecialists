using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public List<GameObject> TopSpawnPoints;
    public List<GameObject> MidSpawnPoints;
    public List<GameObject> BotSpawnPoints;
    public GameObject MeleePrefab;
    public GameObject RangePrefab;
    public GameObject CannonPrefab;
    public GameObject SuperPrefab;

    public int WaveNumber = 0;
    public float WaveTimer = 0;

    public bool MidInhibitor = false;
    public bool TopInhibitor = false;
    public bool BotInhibitor = false;


    private void Start()
    {
        WaveTimer = GameConsts.MINION_WAVE_TIME;
    }
    private void Update()
    {
        SpawnWave();
    }

    void SpawnWave()
    {
        if (InGameManager.Instance.GameTime < GameConsts.MINION_SPAWN_TIME)
            return;

        if(WaveTimer >= GameConsts.MINION_WAVE_TIME)
        {
            System.TimeSpan t = System.TimeSpan.FromSeconds(InGameManager.Instance.GameTime);
            Debug.Log(string.Format("WaveNumber:{0} has spawned at {1}!",
                WaveNumber,
                string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds)));

            //super
            if (BotInhibitor||MidInhibitor||TopInhibitor)
            {
                if (BotInhibitor && MidInhibitor && TopInhibitor)
                {
                    for (int m = 0; m < GameConsts.SUPER_ALL_COUNT; m++)
                    {
                        SpawnUnit(SuperPrefab, GameConsts.SPAWN_TOP);
                        SpawnUnit(SuperPrefab, GameConsts.SPAWN_MID);
                        SpawnUnit(SuperPrefab, GameConsts.SPAWN_BOT);
                    }
                }
                else
                {
                    for (int m = 0; m < GameConsts.SUPER_COUNT; m++)
                    {
                        SpawnUnit(SuperPrefab, GameConsts.SPAWN_TOP);
                        SpawnUnit(SuperPrefab, GameConsts.SPAWN_MID);
                        SpawnUnit(SuperPrefab, GameConsts.SPAWN_BOT);
                    }
                }
            }
            for (int m = 0; m < GameConsts.MELEE_COUNT; m++)
            {
                SpawnUnit(MeleePrefab, GameConsts.SPAWN_TOP);
                SpawnUnit(MeleePrefab, GameConsts.SPAWN_MID);
                SpawnUnit(MeleePrefab, GameConsts.SPAWN_BOT);
            }

            if(InGameManager.Instance.GameTime <= GameConsts.CANNON_FIRST_WAVE)
            {
                //every 3 waves
                if(WaveNumber % 3 == 0)
                {
                    SpawnUnit(CannonPrefab,GameConsts.SPAWN_TOP);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_MID);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_BOT);
                }
            }
            else if(InGameManager.Instance.GameTime > GameConsts.CANNON_FIRST_WAVE
                && InGameManager.Instance.GameTime <= GameConsts.CANNON_SECOND_WAVE)
            {
                if (WaveNumber % 2 == 0)
                {
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_TOP);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_MID);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_BOT);
                }
            }
            else
            {
                for (int m = 0; m < GameConsts.CANNON_COUNT; m++)
                {
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_TOP);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_MID);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_BOT);
                }
            }

            for (int m = 0; m < GameConsts.RANGE_COUNT; m++)
            {
                SpawnUnit(MeleePrefab, GameConsts.SPAWN_TOP);
                SpawnUnit(MeleePrefab, GameConsts.SPAWN_MID);
                SpawnUnit(MeleePrefab, GameConsts.SPAWN_BOT);
            }

            WaveTimer = 0;
            WaveNumber++;
        }
        else
        {
            WaveTimer += Time.deltaTime;
        }
    }

    private void SpawnUnit(GameObject prefab,int spawnLoc)
    {
        GameObject go = Instantiate(prefab,
                SpawnPoints[spawnLoc].transform.position,
                Quaternion.identity);
        Minion minion = go.GetComponent<Minion>();

        switch(spawnLoc)
        {
            case 0:
                {
                    minion.Path = MidSpawnPoints;
                    minion.target = MidSpawnPoints[0].transform;
                    break;
                }
            case 1:
                {
                    minion.Path = BotSpawnPoints;
                    minion.target = BotSpawnPoints[0].transform;
                    break;
                }
            case 2:
                {
                    minion.Path = TopSpawnPoints;
                    minion.target = TopSpawnPoints[0].transform;
                    break;
                }
        }
    }
}
