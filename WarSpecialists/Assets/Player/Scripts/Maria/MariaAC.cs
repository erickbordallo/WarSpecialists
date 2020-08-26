using UnityEngine;

public class MariaAC : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private Animator _animator;
    private bool _isMoving;
    private bool _isAlive;
    private void Start()
    {
        // Set Moveing animation
        _animator = gameObject.GetComponent<Animator>();
        _isMoving = _player.gameObject.GetComponent<PlayerMovement>().IsMoving;
        _animator.SetBool("IsMoving", _isMoving);

        // Set Death animation
        _isAlive = _player.gameObject.GetComponent<PlayerBase>().IsAlive;
        _animator.SetBool("IsAlive", _isAlive);
    }

    private void Update()
    {
        // Set Moveing animation
        _isMoving = _player.gameObject.GetComponent<PlayerMovement>().IsMoving;
        _animator.SetBool("IsMoving", _isMoving);

        // Set Death animation
        _isAlive = _player.gameObject.GetComponent<PlayerBase>().IsAlive;
        _animator.SetBool("IsAlive", _isAlive);
    }
}
