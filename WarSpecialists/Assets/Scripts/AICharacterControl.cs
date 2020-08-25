using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    //[RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }
       // public ThirdPersonCharacter character { get; private set; }
        public Transform target;
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
                //Character.Move(Vector3.zero, false, false);
            }
        }
        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
