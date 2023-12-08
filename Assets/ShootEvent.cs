using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEvent : MonoBehaviourWithPause{

    [SerializeField] ShooterEnemyScript enemy;

    public void Shoot() {
        enemy.Shoot();
    }

}
