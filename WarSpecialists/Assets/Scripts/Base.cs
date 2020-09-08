using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [Header("Attributes")]

    [SerializeField]
    public GameTypes.Team team = GameTypes.Team.Blue;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float baseDamage = 100.0f;
    [SerializeField]
    private float fireDelay = 0.5f;
    private float currentFireTime = 0;

    [SerializeField]
    private List<GameObject> enemyList;
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

                enemyList[0].GetComponent<Minion>().TakeDamage(baseDamage);
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

        if (other.GetComponent<Targetable>() != null && other.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
        {
            if (other.GetComponent<Minion>().team != team)
                enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Targetable>() != null && other.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
        {
            if (other.GetComponent<Minion>().team != team)
                enemyList.Remove(other.gameObject);
        }
    }
}
