using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviourWithPause {

    public static EnemyManager enemyManager { get; private set; }

    Transform enemyTarget;

    List<EnemyAI> enemies;

    public Action OnRoomClear;


    private void Awake(){
        enemyManager = this;
    }

    private void Start(){
        EnemyAI[] enemiesArray = FindObjectsOfType<EnemyAI>();
        enemies = new List<EnemyAI>(enemiesArray);
        foreach (EnemyAI enemy in enemies) {
            enemy.OnEnemyDeath += DecreaseEnemyCount;
        }
    }

    private void Update(){
        if (enemies.Count == 0)
            OnRoomClear?.Invoke();
    }

    void DecreaseEnemyCount(EnemyAI pEnemy) {//increase money as well
        enemies.Remove(pEnemy);
    }

    public void SetEnemyTarget(Transform pWalkTarget, Transform pAttackTarget, bool pPlayer) {
        foreach (EnemyAI enemy in enemies) {
            enemy.SetTarget(pWalkTarget, pAttackTarget);
        }
    }
}
