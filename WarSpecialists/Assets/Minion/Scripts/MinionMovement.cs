﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMovement : MonoBehaviour
{
    public List<GameObject> mWaypoints;
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject targetedEnemy;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private float distanceFromWaypoint = 0.5f;

    private int mCurrentWaypointIndex = 0;

    private float _rotationVelocity;

    [SerializeField]
    private float attackDamage = 10f;

    [SerializeField]
    private float attackDelayTime = 1f;

    private float attackTimer = 0f;

    private List<GameObject> possibleTargets;

    [SerializeField]
    private float distanceFromTarget = 35.0f;

    [SerializeField]
    private List<GameObject> enemyList;

    private GameTypes.Team myTeam;

    float SqrAttackRange;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        SqrAttackRange = attackRange * attackRange;

        //Finding waypoints
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        foreach (GameObject waypoint in waypoints)
        {
            WaypointScript wpt = waypoint.GetComponent<WaypointScript>();
            if (wpt.lane == gameObject.GetComponent<Minion>().lane && wpt.team == gameObject.GetComponent<Targetable>().team)
            {
                mWaypoints.Add(waypoint);
            }
        }
        mWaypoints.Sort((p1, p2) => p1.GetComponent<WaypointScript>().waypointOrder.CompareTo(p2.GetComponent<WaypointScript>().waypointOrder));
        mCurrentWaypointIndex = 0;
        agent.SetDestination(mWaypoints[mCurrentWaypointIndex].transform.position);

        attackRange = 5.0f;

        possibleTargets = new List<GameObject>();
        enemyList = new List<GameObject>();

        myTeam = gameObject.GetComponent<Targetable>().team;
        InvokeRepeating("UpdatePossibleTargets", 1.0f, 2.0f);
    }

    private void UpdatePossibleTargets()
    {
        Targetable[] targets = FindObjectsOfType<Targetable>();

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].gameObject != null && targets[i].team != myTeam)
                possibleTargets.Add(targets[i].gameObject);
        }

        Vector3 myPosition = gameObject.transform.position;

        float SqrDistanceFromTarget = distanceFromTarget * distanceFromTarget;
        for (int i = 0; i < possibleTargets.Count; i++)
        {
            if (possibleTargets[i] != null && (possibleTargets[i].transform.position - myPosition).sqrMagnitude < SqrDistanceFromTarget)
            {
                if (!enemyList.Contains(possibleTargets[i]))
                    enemyList.Add(possibleTargets[i]);
            }
            else
            {
                if (enemyList.Contains(possibleTargets[i]))
                    enemyList.Remove(possibleTargets[i]);
            }
        }

        //sort to attack closest objective in list
        enemyList.Sort(delegate (GameObject a, GameObject b)
        {
            return ((a.transform.position - myPosition).sqrMagnitude)
                .CompareTo((b.transform.position - myPosition).sqrMagnitude);
        });

    }
    private void Update()
    {
        attackTimer -= Time.deltaTime;

        SqrAttackRange = attackRange * attackRange;

        if (enemyList.Count == 0)
        {
            targetedEnemy = null;
        }
        else
        {
            targetedEnemy = enemyList[0];

            if (enemyList[0] == null)
                enemyList.RemoveAt(0);
        }

        if (targetedEnemy != null)
        {
            if ((targetedEnemy.transform.position - gameObject.transform.position).sqrMagnitude > SqrAttackRange)
            {
                //movement.IsMoving = true;
                agent.SetDestination(targetedEnemy.transform.position);
                agent.stoppingDistance = attackRange;

                Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref _rotationVelocity,
                    (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                if (attackTimer < 0f)
                {
                    agent.isStopped = true;

                    if (targetedEnemy.GetComponent<Minion>() != null)
                    {
                        targetedEnemy.GetComponent<Minion>().TakeDamage(attackDamage);
                    }

                    if (targetedEnemy.GetComponent<PlayerBase>() != null)
                    {
                        targetedEnemy.GetComponent<PlayerBase>().TakeDamage(attackDamage);
                    }

                    //if (targetedEnemy.GetComponent<Tower>() != null)
                    //{
                    //    targetedEnemy.GetComponent<Tower>().TakeDamage(attackDamage);
                    //}

                    attackTimer = attackDelayTime;
                }
            }
        }
        else
        {
            agent.SetDestination(mWaypoints[mCurrentWaypointIndex].transform.position);
            agent.isStopped = false;
        }

        if (!agent.pathPending && agent.remainingDistance < distanceFromWaypoint)
        {
            //if they have reach last waypoint stop agent
            if (mCurrentWaypointIndex == mWaypoints.Count - 1)
            {
                agent.isStopped = true;
            }
            else
            {
                GoToNextWaypoint();
            }
        }
    }

    private void GoToNextWaypoint()
    {
        mCurrentWaypointIndex++;
        agent.SetDestination(mWaypoints[mCurrentWaypointIndex].transform.position);
    }
}
