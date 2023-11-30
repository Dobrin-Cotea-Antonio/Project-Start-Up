using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Explosion : MonoBehaviourWithPause{

    [SerializeField] float explosionScale;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask mask;

    HpComponent hp;

    void Start(){
        hp = GetComponent<HpComponent>();
        hp.OnDeath += SpawnExplosion;
    }

    // Update is called once per frame
    protected override void UpdateWithPause(){
        
    }

    void SpawnExplosion() {

        Vector3 position;
        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.down, out hit, 10,mask);
        position = hit.point+new Vector3(0,0.8f,0);

        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);

        VisualEffect effect=explosion.GetComponent<VisualEffect>();

        effect.playRate = 1.75f;

    }
}
