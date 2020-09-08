using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMovement : MonoBehaviour
{
    public List<GameObject> mWaypoints;
    private NavMeshAgent agent;

    [SerializeField]
    private float distanceFromWaypoint = 0.5f;

    private int mCurrentWaypointIndex = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //Finding waypoints
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        foreach(GameObject waypoint in waypoints)
        {
            WaypointScript wpt = waypoint.GetComponent<WaypointScript>();
            if (wpt.lane == gameObject.GetComponent<Minion>().lane && wpt.team == gameObject.GetComponent<Minion>().team)
            {
                mWaypoints.Add(waypoint);
            }
        }
        mWaypoints.Sort((p1, p2) => p1.GetComponent<WaypointScript>().waypointOrder.CompareTo(p2.GetComponent<WaypointScript>().waypointOrder));
        mCurrentWaypointIndex = 0;
        agent.SetDestination(mWaypoints[mCurrentWaypointIndex].transform.position);
    }


    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < distanceFromWaypoint)
        {
            GoToNextWaypoint();
        }
    }        

    private void GoToNextWaypoint()
    {
        mCurrentWaypointIndex++;
        // This code is temporary. Right now, if the unit reaches the end, it will me destroyed.
        if (mCurrentWaypointIndex >= mWaypoints.Count)
        {
            //Destroy(gameObject);
            return;
        }
        agent.SetDestination(mWaypoints[mCurrentWaypointIndex].transform.position);
    }
}
