using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
//[RequireComponent(typeof(ThirdPersonCharacter))]
public class Minion : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    // public ThirdPersonCharacter character { get; private set; }
    public Transform target;
    public List<GameObject> Path;
    public int PathCount = 0;
    void Start()
    {
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        // CharacterController = GetComponent<ThirdPersonCharacter>();
        agent.updateRotation = false;
        agent.updatePosition = true;
    }


    private void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            //Character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            PathCount++;
            target = Path[PathCount].transform;
        }
    }
}
