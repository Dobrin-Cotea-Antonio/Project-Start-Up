using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyScript : EnemyAI{


    [Header("Shooting Data")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPosition;
    [SerializeField] float aimSpread;

    [Header("Bullet Data")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletRange;
    [SerializeField] float bulletDamage;

    protected override void Start(){
        base.Start();
        canLeaveAttackState = true;
    }


    protected override void Attack(){

        float angle = Vector3.Angle((shootPosition.position - targetTransform.position).normalized, transform.forward);
        //Debug.Log(angle);

        if (!(angle>15||angle<165)) { 
            return;
        }


        lastAttackTime = Time.time;
        GameObject b1 = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);

        Bullet bullet1 = b1.GetComponent<Bullet>();
        bullet1.speed = bulletSpeed;
        bullet1.range = bulletRange;
        bullet1.damage = bulletDamage;

        float randomAngle = Random.Range(-aimSpread, aimSpread);
        Debug.Log(randomAngle);
        bullet1.transform.forward = Quaternion.Euler(0, 90 + randomAngle, 0) * shootPosition.forward;

        Vector3 dir = (targetTransform.position - shootPosition.position).normalized;

        dir = Quaternion.Euler(0,randomAngle,0) * dir;

        bullet1.AddSpeed(dir);

    }


}
