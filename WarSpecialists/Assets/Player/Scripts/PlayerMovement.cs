using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeedMovement;
    
    private float _moveSpeed;
    private NavMeshAgent agent;
    private float _rotationVelocity;
    public bool IsMoving { get; private set; }
    private void Start()
    {
        _moveSpeed = gameObject.GetComponent<PlayerBase>().MoveSpeed;
        agent = gameObject.GetComponent<NavMeshAgent>();
        _rotationSpeedMovement = 0.075f;
        agent.speed = _moveSpeed;
        agent.acceleration = _moveSpeed;
        IsMoving = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Moving(Input.mousePosition);
            IsMoving = true;
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    IsMoving = false;
                }
            }
        }

    }

    private void Moving(Vector3 mousePosition)
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out hit, Mathf.Infinity))
        {
            agent.SetDestination(hit.point);

            Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref _rotationVelocity,
                _rotationSpeedMovement * (Time.deltaTime * 5));

            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
    }
}
