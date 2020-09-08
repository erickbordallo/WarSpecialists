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
    public float _rotationVelocity;
    public bool IsMoving { get; set; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }

    private HeroCombat heroCombat;

    private void Start()
    {
        _moveSpeed = gameObject.GetComponent<PlayerBase>().MoveSpeed;
        Agent = gameObject.GetComponent<NavMeshAgent>();
        heroCombat = gameObject.GetComponent<HeroCombat>();
        _rotationSpeedMovement = 0.075f;
        Agent.speed = _moveSpeed;
        Agent.acceleration = _moveSpeed;
        IsMoving = false;
    }

    private void Update()
    {
        if (heroCombat.targetedEnemy != null)
        {
            if (heroCombat.targetedEnemy.GetComponent<HeroCombat>() != null)
            {
                if (heroCombat.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    heroCombat.targetedEnemy = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Moving(Input.mousePosition);
            IsMoving = true;
        }

        if (heroCombat.IsAttacking)
        {
            Agent.isStopped = true;
        }
        else
        {
            Agent.isStopped = false;
        }

        if (!Agent.pathPending)
        {
            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
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
            if (hit.collider.tag == "Floor")
            {
                Agent.SetDestination(hit.point);
                heroCombat.targetedEnemy = null;
                agent.stoppingDistance = 0f;

                Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref _rotationVelocity,
                    _rotationSpeedMovement * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
        }
    }
}
