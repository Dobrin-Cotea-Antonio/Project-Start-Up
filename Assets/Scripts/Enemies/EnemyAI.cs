using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviourWithPause {

    [Header("Helper Stuff")]
    protected NavMeshAgent agent;
    protected Transform targetTransformMove;
    protected Transform targetTransformAttack;
    protected float distanceToPlayer;
    protected float lastAttackTime = -100000;
    protected bool canLeaveAttackState = true;
    protected float chaseDistanceMultiplier;
    protected float baseChaseRange;

    [Header("Animator")]
    [SerializeField] float musicMultiplier=2;
    [SerializeField] protected Animator animator;

    [Header("Stats")]
    [SerializeField] float speed;

    [Header("Chase")]
    [SerializeField] bool canLoseAgro;
    [SerializeField] float chaseRange;

    [Header("Attack")]
    [SerializeField] float attackRange;
    [SerializeField] float attackCooldown;
    

    public enum EnemyStates {
        Idle,
        Chase,
        Attack
    }

    public EnemyStates state { get; private set; }
    public Action<EnemyAI> OnEnemyDeath;

    protected virtual void Start(){
        state = EnemyStates.Idle;

        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;

        baseChaseRange = chaseRange;

    }

    protected override void UpdateWithPause(){
        if (targetTransformMove == null) {
            targetTransformMove = GameManager.gameManager.player.transform;
            targetTransformAttack = GameManager.gameManager.player.GetComponent<PlayerControls>().GetRefPoint();
        }

        chaseDistanceMultiplier = 1 + Mathf.Min(MusicHandler.musicHandler._musicTransitionProgress, 1 )* (musicMultiplier);
        //Debug.Log(chaseDistanceMultiplier);
        
        chaseRange = baseChaseRange * chaseDistanceMultiplier;

        distanceToPlayer = (targetTransformMove.position - transform.position).magnitude;

        StateMachine();

        AnimationStateMachine();
    }

    protected virtual void AnimationStateMachine() { }

    void StateMachine() {
        switch (state) {
            case EnemyStates.Idle:
                IdleState();
                break;
            case EnemyStates.Chase:
                ChaseState();
                break;
            case EnemyStates.Attack:
                AttackState();
                break;
        }
    
    }

    void IdleState() {

        if (distanceToPlayer <= chaseRange) {
            state = EnemyStates.Chase;
            return;
        }

    }

    void ChaseState() {

        if (canLoseAgro && distanceToPlayer < chaseRange) {
            state = EnemyStates.Idle;
            return;
        }

        if (distanceToPlayer <= attackRange) {
            state = EnemyStates.Attack;
            return;
        }

        agent.SetDestination(targetTransformMove.position);

    }

    void AttackState() {

        agent.SetDestination(transform.position);
        transform.LookAt(targetTransformMove);

        if (canLeaveAttackState && distanceToPlayer > attackRange) {
            state = EnemyStates.Chase;
            return;
        }

        if (Time.time - lastAttackTime > attackCooldown) {
            Attack();
        }
        
    }

    protected virtual void PlayIdleAnimation() { }

    protected virtual void PlayChaseAnimation() { }

    protected virtual void PlayAttackAnimation() { } 

    protected virtual void Attack() {}

    private void OnDestroy(){
        MusicHandler.musicHandler.AddMusicIntensity(MusicHandler.musicHandler._enemyDeathIntensity);
        OnEnemyDeath!.Invoke(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    }

    public void SetTarget(Transform pWalkTransform,Transform pAttackTransform) {
        targetTransformMove = pWalkTransform;
        targetTransformAttack = pAttackTransform;
    }
}
