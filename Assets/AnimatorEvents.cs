using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviourWithPause{
    [SerializeField] Aim aim;

    public void ShootBullet() {
        aim.ShootBullet();
    }
}
