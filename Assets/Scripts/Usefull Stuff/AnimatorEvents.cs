using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviourWithPause{
    [SerializeField] Aim aim;

    Animator animator;

    private void Start(){
        animator=GetComponent<Animator>();
    }

    public void ShootBullet() {
        aim.ShootBullet();
    }

    public void EnableShooting() {
        animator.SetBool("canShoot", true);
    }
}
