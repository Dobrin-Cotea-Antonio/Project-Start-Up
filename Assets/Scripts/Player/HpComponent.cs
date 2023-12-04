using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HpComponent : MonoBehaviourWithPause{

    [SerializeField] float maxHp;
    public float currentHp { get; private set; }

    public event Action<float, float> OnDamageTaken;
    public event Action OnDeath;

    ExplosionCollider explosionCollider;
    private void Start(){
        currentHp = maxHp;
        OnDamageTaken?.Invoke(currentHp, maxHp);
    }

    public void TakeDamage(float pDamage,ExplosionCollider explColl=null) {

        if (!isActiveAndEnabled)
            return;

        if (explColl != null) {
            if (explosionCollider == explColl) {
                return;
            }
            explosionCollider = explColl;
        }

        currentHp = Mathf.Max(0,currentHp-pDamage);
        OnDamageTaken?.Invoke(currentHp, maxHp);
        if (currentHp == 0) {
            OnDeath?.Invoke();
            Destroy(transform.root.gameObject);
        }
    }

    public void RestoreHp(float pHp) {
        currentHp = Mathf.Min(currentHp + pHp, maxHp);
        OnDamageTaken?.Invoke(currentHp, maxHp);
    }

}
