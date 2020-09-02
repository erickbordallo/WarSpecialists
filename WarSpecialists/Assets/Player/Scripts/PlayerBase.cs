using UnityEngine;
using UnityEngine.AI;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _AttackSpeed;
    [SerializeField]
    private float _AttackRange;
    public bool IsAlive { get { return Health > 0; } }
    public int Health { get => _health; set => _health = value; }
    public int Attack { get; protected set; }
    public int Damage { get; protected set; }
    public int Deffense { get; protected set; }
    public int SpecialtyPoints { get; protected set; }
    public int Gold { get; protected set; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float AttackSpeed { get => _AttackSpeed; set => _AttackSpeed = value; }
    public float AttackRange { get => _AttackRange; set => _AttackRange = value; }

    virtual protected void Start() { }
    virtual protected void Update() { }

    protected void Died()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }
}
