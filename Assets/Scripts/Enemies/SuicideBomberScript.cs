using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SuicideBomberScript : EnemyAI{

    [Header("Explosion Data")]
    [SerializeField] HpComponent hp;
    [SerializeField] float explosionScale;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask mask;
    [SerializeField] float bombFuse;

    protected override void Start(){
        base.Start();

        hp.OnDeath += SpawnExplosion;
        canLeaveAttackState = false;
    }

    public void SpawnExplosion(){
        MusicHandler.musicHandler.AddMusicIntensity(MusicHandler.musicHandler._explosionIntensity);
        Vector3 position;
        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.down, out hit, 10, mask);
        position = hit.point + new Vector3(0, 1.15f, 0) * explosionScale;

        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);

        VisualEffect effect = explosion.GetComponent<VisualEffect>();

        effect.playRate = 1.75f;

        Destroy(transform.root.gameObject);

    }

    protected override void PlayAttackAnimation(){
        animator.SetBool("Rolling", false);
    }

    protected override void PlayChaseAnimation(){
        animator.SetBool("Rolling", true);
    }

    protected override void AnimationStateMachine(){
        switch (state) {
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

}
