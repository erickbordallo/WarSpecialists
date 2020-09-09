using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject blueTeamMinionPrefab;

    [SerializeField]
    private GameObject redTeamMinionPrefab;

    [SerializeField]
    private int maxQuantity;
    private int currentQuantity;
    [SerializeField]
    private float waveSpawnDelay = 30;
    private float currentSpawnTime = 0;
    [SerializeField]
    private float spawnCadency = 1.0f;
    private float currentCadency = 0.0f;

    public GameTypes.Team team = GameTypes.Team.Blue;
    public GameTypes.Lane lane = GameTypes.Lane.Bottom;

    private void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if(currentSpawnTime >= waveSpawnDelay)
        {
            SpawnMinionWave();
        }
    }

    private void SpawnMinionWave()
    {
        currentCadency += Time.deltaTime;
        if(currentCadency >= spawnCadency && currentQuantity < maxQuantity)
        {
            SpawnMinion();
            currentQuantity++;
            if(currentQuantity >= maxQuantity)
            {
                currentSpawnTime = 0.0f;
                currentQuantity = 0;
            }
            currentCadency = 0.0f;
        }
    }

    private void SpawnMinion()
    {
        Vector3 spawnPos = transform.position;
        if(team == GameTypes.Team.Blue)
        {
            GameObject blueMinion = Instantiate(blueTeamMinionPrefab, spawnPos, transform.rotation);
            blueMinion.GetComponent<Minion>().lane = lane;
            blueMinion.GetComponent<Targetable>().team = team;
        }
        else
        {
            GameObject redMinion = Instantiate(redTeamMinionPrefab, spawnPos, transform.rotation);
            redMinion.GetComponent<Minion>().lane = lane;
            redMinion.GetComponent<Targetable>().team = team;
        }   
    }
}
