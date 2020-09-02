using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    public GameObject[] m_Sign;
    
    private NavMeshAgent agent;
    
    private float distanceFromPatrolNode = 2.0f;
    //private float startingTime = 1.0f;
    //private float stoppingTime;
    public Transform[] patrolNodes;
    private int currentPatrolNodeIndex = 0;
    private int count = 0;
    public GameTypes.MinionLane lane;

    void Start()
    {
        m_Sign = GameObject.FindGameObjectsWithTag("node");
        agent = GetComponent<NavMeshAgent>();

        for (int i = 0; i < m_Sign.Length; ++i)
        {
            Debug.Log(i);
            Debug.Log(m_Sign[i].name);
        }

        if (lane == m_Sign[0].GetComponent<WaypointScript>().lane)//blue left
        {
            currentPatrolNodeIndex = 0;
            agent.SetDestination(m_Sign[currentPatrolNodeIndex].transform.position);
        }
        else if (lane == m_Sign[5].GetComponent<WaypointScript>().lane)//blue mid
        {
            currentPatrolNodeIndex = 5;
            agent.SetDestination(m_Sign[currentPatrolNodeIndex].transform.position);
        }
        else if(lane == m_Sign[10].GetComponent<WaypointScript>().lane)
        {
            currentPatrolNodeIndex = 10;
            agent.SetDestination(m_Sign[currentPatrolNodeIndex].transform.position);
        }
        else if (lane == m_Sign[15].GetComponent<WaypointScript>().lane)//red left
        {
            currentPatrolNodeIndex = 15;
            agent.SetDestination(m_Sign[currentPatrolNodeIndex].transform.position);
        }
        else if (lane == m_Sign[20].GetComponent<WaypointScript>().lane)//red mid
        {
            currentPatrolNodeIndex = 20;
            agent.SetDestination(m_Sign[currentPatrolNodeIndex].transform.position);
        }
        else if (lane == m_Sign[25].GetComponent<WaypointScript>().lane)//red right
        {
            currentPatrolNodeIndex = 25;
            agent.SetDestination(m_Sign[currentPatrolNodeIndex].transform.position);
        }
    }


    private void Update()
    { 
    //    float distance = Vector3.Distance(transform.position, patrolNodes[currentPatrolNodeIndex].position);
        if (!agent.pathPending && agent.remainingDistance < distanceFromPatrolNode )
        {  
            
            UpdatePatrolNodeIndex();
            count++;
            
        }
        
    }
    //
    private void UpdatePatrolNodeIndex()
    {
        if (count < 4)
        {
            currentPatrolNodeIndex = (currentPatrolNodeIndex + 1) % m_Sign.Length;
            agent.SetDestination(m_Sign[currentPatrolNodeIndex].transform.position);
            Debug.Log(count);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
    //{
    //    currentPatrolNodeIndex = (currentPatrolNodeIndex + 1) % patrolNodes.Length;
    //    agent.SetDestination(patrolNodes[currentPatrolNodeIndex].position);
    //}
    //public float getPatrolNodeDistance()
    //{
    //    return distanceFromPatrolNode;
    //}
}
