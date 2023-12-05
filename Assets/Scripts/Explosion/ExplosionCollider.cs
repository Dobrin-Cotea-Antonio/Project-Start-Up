using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosionCollider : MonoBehaviourWithPause{
    [SerializeField] int explosionDamage;
    [SerializeField] float timeToTakeDamage;
    [SerializeField] DestroyExplosionWhenDone explosionData;

    private void OnTriggerEnter(Collider other){
        if (explosionData.timeSinceStart > timeToTakeDamage)
            return;

        HpComponent hp = other.gameObject.GetComponent<HpComponent>();
        if (hp != null){
            Debug.Log(hp.gameObject);
            hp.TakeDamage(explosionDamage, this);
        }
    }
}
