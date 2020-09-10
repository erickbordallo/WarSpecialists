using UnityEngine;

public class MariaAC : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private Animator _animator;
    private bool _isMoving;
    private bool _isAlive;
    private bool _isAttack;
    private void Start()
    {
        // Set Moveing animation
        _animator = gameObject.GetComponent<Animator>();
        _isMoving = _player.gameObject.GetComponent<PlayerMovement>().IsMoving;
        _animator.SetBool("IsMoving", _isMoving);

        // Set Death animation
        _isAlive = _player.gameObject.GetComponent<PlayerBase>().IsAlive;
        _animator.SetBool("IsAlive", _isAlive);

        _isAttack = _player.gameObject.GetComponent<HeroCombat>().IsAttacking;
        _animator.SetBool("IsAttacking", _isAttack);
    }

    private void Update()
    {
        // Set Moveing animation
        _isMoving = _player.gameObject.GetComponent<PlayerMovement>().IsMoving;
        _animator.SetBool("IsMoving", _isMoving);

        // Set Death animation
        _isAlive = _player.gameObject.GetComponent<PlayerBase>().IsAlive;
        _animator.SetBool("IsAlive", _isAlive);

        // Set Attack animation
        _isAttack = _player.gameObject.GetComponent<HeroCombat>().IsAttacking;
        _animator.SetBool("IsAttacking", _isAttack);
    }

    //function to do the damage, executed by events in the attack animation 
    //see events of animation tab in the imported attack prefab
    public void SwordDamage()
    {
        _player.gameObject.GetComponent<HeroCombat>().DoDamage();
    }

    //function set the attacking to false, executed by events in the attack animation 
    //see events of animation tab in the imported attack prefab
    public void AnimationEnds()
    {
        _player.gameObject.GetComponent<HeroCombat>().IsAttacking = false;
    }
}
