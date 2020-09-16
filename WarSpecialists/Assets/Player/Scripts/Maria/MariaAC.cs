using UnityEngine;

public class MariaAC : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private AnimationClip ability1;

    private Animator _animator;
    private bool _isMoving;
    private bool _isAlive;
    private bool _isAttack;
    private bool _abilityCasting;
    private float _abilityCastingTime;
    private float _abilityCastingTimer;
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

        _abilityCasting = _player.gameObject.GetComponent<MariaAbility>().AbilityCasting;
        _animator.SetBool("AbilityCasting", _abilityCasting);

        _abilityCastingTime = ability1.length;
        _abilityCastingTimer = 0;
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

        _abilityCasting = _player.gameObject.GetComponent<MariaAbility>().AbilityCasting;
        _animator.SetBool("AbilityCasting", _abilityCasting);

        if (_abilityCasting && _abilityCastingTimer <= 0)
        {
            _abilityCastingTimer = _abilityCastingTime;
            _player.gameObject.GetComponent<MariaAbility>().AbilityCasting = false;
            _player.gameObject.GetComponent<PlayerMovement>().Agent.speed = _player.gameObject.GetComponent<PlayerMovement>().MoveSpeed;
            _player.gameObject.GetComponent<PlayerMovement>().Agent.acceleration = _player.gameObject.GetComponent<PlayerMovement>().MoveSpeed;
        }

        if(_abilityCasting && _abilityCastingTimer > 0)
        {
            _abilityCastingTimer -= Time.deltaTime;
        }
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
