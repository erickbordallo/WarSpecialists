using UnityEngine;
using UnityEngine.AI;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _AttackSpeed;
    [SerializeField]
    private float _AttackRange;
    [SerializeField]
    private float _Attack;
    public bool IsAlive { get { return Health > 0; } }
    public float Health { get => _health; set => _health = value; }
    public int Damage { get; protected set; }
    public int Deffense { get; protected set; }
    public int SpecialtyPoints { get; protected set; }
    public int Gold { get; protected set; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float AttackSpeed { get => _AttackSpeed; set => _AttackSpeed = value; }
    public float AttackRange { get => _AttackRange; set => _AttackRange = value; }
    public float Attack{ get => _Attack; set => _Attack = value; }

    virtual protected void Start() { }
    virtual protected void Update() { }

    protected void Died()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void TakeDamage(float _damage)
    {
        _damage -= Deffense;
        _health -= _damage;
        if (_health <= 0.0f)
        {
            //Destroy(gameObject);
        }
    }

    public void DestroyHeroObject()
    {
        Destroy(gameObject);
    }
}
