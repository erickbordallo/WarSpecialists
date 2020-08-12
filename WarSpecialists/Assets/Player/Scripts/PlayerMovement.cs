using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
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
        if (!IsBlocked(mousePosition))
        {
            // Moving
        }
    }

    private bool IsBlocked(Vector3 mousePosition)
    {
        // Check isblocked
        return true;
    }
}
