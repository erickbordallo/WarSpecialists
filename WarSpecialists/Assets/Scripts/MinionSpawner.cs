using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject minionSpawnee;
    [SerializeField]
    private int quantity;
    [SerializeField]
    private bool stopSpawns = false;
    [SerializeField]
    private float initialSpawnTime = 0;
    [SerializeField]
    private float spawnDelay = 30;

    private float boundZ;
    // Start is called before the first frame update
    void Start()
    {
        //this variable helps to determine the z position when we have more than one spawn object, 
        //so they dont spawn in the same position of another instance
        boundZ = minionSpawnee.GetComponent<Renderer>().bounds.size.z * 1.5f;
        
        InvokeRepeating("SpawnMinion", initialSpawnTime, spawnDelay);
    }

    void SpawnMinion()
    {
        for(int i = 0; i< quantity; ++i)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z+(i* boundZ));
            Instantiate(minionSpawnee, spawnPos, transform.rotation);
        }
        if (stopSpawns)
        {
            CancelInvoke("SpawnMinion");
        }
    }
}
