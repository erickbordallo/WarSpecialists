using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _rotationSpeedMovement;
    
    private NavMeshAgent agent;
    private float _rotationVelocity;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        _rotationSpeedMovement = 0.075f;
        agent.speed = _moveSpeed;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Moving(Input.mousePosition);
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
