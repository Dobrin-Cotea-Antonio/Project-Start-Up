using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviourWithPause {

    [Header("Helper Stuff")]
    protected NavMeshAgent agent;
    protected Transform targetTransform;
    protected MeshRenderer enemyRenderer;
    protected Material material;
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

    protected virtual void Start(){
        state = EnemyStates.Idle;

        agent = GetComponent<NavMeshAgent>();
        enemyRenderer = GetComponent<MeshRenderer>();

        List<Material> materials = new List<Material>();
        enemyRenderer.GetMaterials(materials);
        material = materials[0];

        agent.speed = speed;

        Debug.Log("starting");
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

        if (distanceToPlayer <= chaseRange) {
            state = EnemyStates.Chase;
            material.color = Color.yellow;
            return;
        }

    }

    void ChaseState() {

        if (canLoseAgro && distanceToPlayer < chaseRange) {
            material.color = Color.green;
            state = EnemyStates.Idle;
            return;
        }

        if (distanceToPlayer <= attackRange) {
            material.color = Color.red;
            state = EnemyStates.Attack;
            return;
        }

        agent.SetDestination(targetTransform.position);

    }

    void AttackState() {

        agent.SetDestination(transform.position);
        transform.LookAt(targetTransform);

        if (canLeaveAttackState && distanceToPlayer > attackRange) {
            material.color = Color.yellow;
            state = EnemyStates.Chase;
            return;
        }

        if (Time.time - lastAttackTime > attackCooldown) {
            Attack();
            lastAttackTime = Time.time;
        }
        
    }

    protected virtual void Attack() {}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    }
}
