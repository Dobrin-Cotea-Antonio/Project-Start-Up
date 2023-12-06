using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyScript : EnemyAI{

    [Header("Shooting Data")]
    [SerializeField] GameObject muzzleFlashPrefab;
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
        lastAttackTime = float.MaxValue;
        
        //targetTransform = GameManager.gameManager.player.GetComponent<PlayerControls>().GetRefPoint();
    }


    protected override void AnimationStateMachine(){
        switch (state){
            case EnemyStates.Idle:

                break;
            case EnemyStates.Chase:
                PlayChaseAnimation();
                break;
            case EnemyStates.Attack:
                PlayAttackAnimation();
                break;

        }
    }

    public void Shoot(){

        //float angle = Vector3.Angle((shootPosition.position - targetTransform.position).normalized, transform.forward);
        
        //Debug.Log(targetTransformMove);

        lastAttackTime = float.MaxValue;
        GameObject b1 = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        Instantiate(muzzleFlashPrefab, shootPosition.position, shootPosition.rotation,shootPosition);

        Bullet bullet1 = b1.GetComponent<Bullet>();
        bullet1.speed = bulletSpeed;
        bullet1.range = bulletRange;
        bullet1.damage = bulletDamage;

        float randomAngle = Random.Range(-aimSpread, aimSpread);
        Debug.Log(randomAngle);
        bullet1.transform.forward = Quaternion.Euler(0, 180 + randomAngle, 0) * shootPosition.forward;

        Vector3 dir = (targetTransformAttack.position - shootPosition.position).normalized;

        dir = Quaternion.Euler(0,randomAngle,0) * dir;

        bullet1.AddSpeed(dir);

    }
    
    

    protected override void PlayAttackAnimation(){
        animator.SetBool("canShoot", true);
        animator.SetBool("canWalk", false);
    }

    protected override void PlayChaseAnimation()
    {
        animator.SetBool("canWalk", true);
        animator.SetBool("raiseWeapon",true);
        animator.SetBool("canShoot", false);
    }


}
