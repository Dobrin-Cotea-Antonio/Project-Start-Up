using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DestroyExplosionWhenDone : MonoBehaviourWithPause{

    VisualEffect explosion;

    public float timeSinceStart { get; private set; }

    private void Start(){
        explosion = GetComponent<VisualEffect>();
    }
    protected override void UpdateWithPause(){

        if (explosion.aliveParticleCount > 0)
            timeSinceStart += Time.deltaTime;

        if (explosion.aliveParticleCount == 0 && timeSinceStart > 0)
            Destroy(this.gameObject);

    }
}
