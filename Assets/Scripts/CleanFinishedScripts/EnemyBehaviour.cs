using RichiGames;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBehaviour : MonoBehaviour
{
    // Init
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
    protected RewindableEnemy _rewindableEnemy;

    // Updated in run time
    private Vector3 lastVelocity;
    private bool _isStopped;

    private void OnEnable()
    {
        TimeController.Instance.OnContinueTime += ContinueTime;
        TimeController.Instance.OnStopTime += StopTime;
    }
    private void OnDisable()
    {
        TimeController.Instance.OnContinueTime -= ContinueTime;
        TimeController.Instance.OnStopTime -= StopTime;
    }
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<BasicEnemy>();
        _rewindableEnemy = GetComponent<RewindableEnemy>();
        _startingPosition = gameObject.transform.position;
        //_attackTimer = new ValueTimer();

        if (_player == null || _agent == null || _entity == null || _startingPosition == null)
        {
            Debug.LogError("EnemyBehavior has failed at the Start set up -> " + gameObject.name);
        }
    }

    // When the time is stopped, this is called one time to Freez the enemy
    public void StopTime()
    {
        Debug.Log("StopTime");
        if (TimeController.Instance.IsStoppedTime)
        {
            if (!_agent.isStopped)
            {
                _agent.isStopped = true;
                _agent.velocity = Vector3.zero;
            }
            lastVelocity = _agent.velocity;
            _isStopped = true;
        }
    }

    // When the time continue, this is called one time to Unfreez the enemy
    public void ContinueTime()
    {
        Debug.Log("ContinueTime");
        if (!TimeController.Instance.IsStoppedTime && _agent.isStopped == true)
        {
            _agent.isStopped = false;
            _agent.velocity = lastVelocity;
            _isStopped = false;
        }
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
            _rewindableEnemy.TimeBetweenAttacks += Time.deltaTime;
            if (_rewindableEnemy.TimeBetweenAttacks > timeBetweenAttacks)
            {
                isReadyToAttack = true;
            }
        }
    }
    public virtual void IdleBehavior()
    {
        _enemyState = "IDLE";
        // Animation - TODO
        //if (_entity.Animator)
        //{
        //    _entity.Animator.SetBool("Run Forward", false); // TODO
        //    _entity.Animator.SetBool("Idle", true); // TODO
        //}

        // Returns the Enemy to the StartingPosition
        _agent.SetDestination(_startingPosition);
    }

    public virtual void ChasePlayer()
    {
        _enemyState = "CHASE";
        // Animation - TODO
        //if (_entity.Animator)
        //{
        //    _entity.Animator.SetBool("Run Forward", true); // TODO
        //    _entity.Animator.SetBool("Idle", false); // TODO
        //}

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
            // Animation - TODO
            //if (_entity.Animator)
            //{
            //    _entity.Animator.SetTrigger("Attack1");
            //    _entity.Animator.SetBool("Run Forward", false); // TODO
            //    _entity.Animator.SetBool("Idle", false); // TODO
            //    ParticleSystem rb = Instantiate(_entity.AttackParticle, _entity.AttackParticlePosition.position, _entity.AttackParticlePosition.rotation);
            //}
            _entity.Attack();
            _rewindableEnemy.TimeBetweenAttacks = 0;
            isReadyToAttack = false;
            //Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    //private void ResetAttack()
    //{
    //    canAttack = false;
    //}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

