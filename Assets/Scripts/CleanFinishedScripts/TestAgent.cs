using RichiGames;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;


public class TestAgent : MonoBehaviour
{// Init
    [Header("DefaultValues")]
    [SerializeField] protected LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] protected string _enemyState;

    //Attacking
    [Header("AttackValues")]
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected bool isReadyToAttack;
    [SerializeField] protected float actualTimeBetweenAttacks;
    //private ValueTimer _attackTimer;

    //States
    [Header("StatesValues")]
    [SerializeField] protected float sightRange, attackRange;
    [SerializeField] protected bool playerInSightRange, playerInAttackRange;

    // Set up at the START
    protected NavMeshAgent _agent;
    protected Transform _player;
    protected Vector3 _startingPosition;
    protected BasicEnemy _entity;

    // Updated in run time
    private Vector3 lastVelocity;
    private bool _isStopped;

    private void OnEnable()
    {
        TimeController.Instance.OnContinueTime += ContinueTime;
        TimeController.Instance.OnContinueTime += StopTime;
    }
    private void OnDisable()
    {
        TimeController.Instance.OnContinueTime -= ContinueTime;
        TimeController.Instance.OnContinueTime -= StopTime;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        //_entity = GetComponent<BasicEnemy>();
        _startingPosition = gameObject.transform.position;
        //_attackTimer = new ValueTimer();
    }

    public void TimeStopCheck()
    {
        if (TimeController.Instance.IsStoppedTime)
        {
            StopTime();
        }
        else
        {
            ContinueTime();
        }
    }

    // When the time is stopped, this is called one time to Freez the enemy
    public void StopTime()
    {
        if (!_agent.isStopped)
        {
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            lastVelocity = _agent.velocity;
            _isStopped = true;
        }
        return;
    }

    // When the time continue, this is called one time to Unfreez the enemy
    public void ContinueTime()
    {
        _agent.isStopped = false;
        _agent.velocity = lastVelocity;
        _isStopped = false;
        _agent.SetDestination(_player.position);
    }
    private void Update()
    {
        if (TimeController.Instance.IsStoppedTime) return;
        
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Changes the Enemy behavior

        if (!playerInSightRange && !playerInAttackRange) IdleBehavior();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        // AttackTimer
        if (!isReadyToAttack)
        {
            actualTimeBetweenAttacks += Time.deltaTime;
            if (actualTimeBetweenAttacks > timeBetweenAttacks)
            {
                isReadyToAttack = true;
            }
        }
    }
    public virtual void IdleBehavior()
    {
        _enemyState = "IDLE";

        // Returns the Enemy to the StartingPosition
        _agent.SetDestination(_startingPosition);
    }

    public virtual void ChasePlayer()
    {
        _enemyState = "CHASE";

        // The Enemy follows the Player
        _agent.SetDestination(_player.position);
        transform.LookAt(_player);
    }

    public virtual void AttackPlayer()
    {
        _enemyState = "ATTCK_PLAYER";
        // Stops the Enemy movement
        _agent.SetDestination(transform.position);
        transform.LookAt(_player);

        // Attack Controller
        if (isReadyToAttack)
        {
            actualTimeBetweenAttacks = 0;
            isReadyToAttack = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

