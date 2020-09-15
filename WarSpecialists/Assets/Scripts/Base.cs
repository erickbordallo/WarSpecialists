using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [Header("Attributes")]

    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float baseDamage = 100.0f;
    [SerializeField]
    private float fireDelay = 0.5f;
    private float currentFireTime = 0;

    private List<GameObject> possibleTargets;

    [SerializeField]
    private List<GameObject> enemyList;

    private void Start()
    {
        enemyList = new List<GameObject>();
    }

    void Update()
    {
        currentFireTime += Time.deltaTime;
        if (enemyList.Count == 0)
        {
            return;
        }

        //checks if first element of the list is still in game if found apply damage, otherwise remove it from list
        //TODO: instead of checking first element, loop list and find first object(minion) found
        if (enemyList[0] != null)
        {
            if (currentFireTime >= fireDelay)
            {
                //small explosion might need some position adjustment.
                if (explosionPrefab != null)
                {
                    GameObject explosion = Instantiate(explosionPrefab, enemyList[0].GetComponent<Transform>().position + new Vector3(0, 6, 0), Quaternion.identity) as GameObject;
                    Destroy(explosion, .5f);
                }

                Minion m = enemyList[0].GetComponent<Minion>();
                PlayerBase player = enemyList[0].GetComponent<PlayerBase>();
                if (m != null)
                {
                    m.TakeDamage(baseDamage);
                }
                if (player != null)
                {
                    player.TakeDamage(baseDamage);
                }
                currentFireTime = 0;
            }
        }
        else
        {
            enemyList.RemoveAt(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Targetable tg = other.GetComponent<Targetable>();
        if (tg != null && tg.team != gameObject.GetComponent<Targetable>().team)
        { 
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Targetable tg = other.GetComponent<Targetable>();
        if (tg != null && tg.team != gameObject.GetComponent<Targetable>().team)
        {
            enemyList.Remove(other.gameObject);
        }
    }

}
