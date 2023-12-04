using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviourWithPause {

    [Header("Helper Stuff")]
    protected NavMeshAgent agent;
    protected Transform targetTransform;
    protected float distanceToPlayer;
    protected float lastAttackTime = -100000;
    protected bool canLeaveAttackState = true;

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

    }

    protected override void UpdateWithPause(){
        if (targetTransform == null) {
            targetTransform = GameManager.gameManager.player.transform;
        }

        distanceToPlayer = (targetTransform.position - transform.position).magnitude;

        StateMachine();

    }

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

        PlayIdleAnimation();

        if (distanceToPlayer <= chaseRange) {
            state = EnemyStates.Chase;
            return;
        }

    }

    void ChaseState() {

        PlayChaseAnimation();

        if (canLoseAgro && distanceToPlayer < chaseRange) {
            state = EnemyStates.Idle;
            return;
        }

        if (distanceToPlayer <= attackRange) {
            state = EnemyStates.Attack;
            return;
        }

        agent.SetDestination(targetTransform.position);

    }

    void AttackState() {

        agent.SetDestination(transform.position);
        transform.LookAt(targetTransform);

        if (canLeaveAttackState && distanceToPlayer > attackRange) {
            state = EnemyStates.Chase;
            return;
        }

        if (agent.velocity.magnitude < 0.01)
        {
            PlayIdleAnimation();
        }
        else {
            PlayChaseAnimation();
        }

        if (Time.time - lastAttackTime > attackCooldown) {
            PlayAttackAnimation();
            Attack();
        }
        
    }

    protected virtual void PlayIdleAnimation() { }

    protected virtual void PlayChaseAnimation() { }

    protected virtual void PlayAttackAnimation() { } 

    protected virtual void Attack() {}

    private void OnDestroy(){
        OnEnemyDeath!.Invoke(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    }
}
