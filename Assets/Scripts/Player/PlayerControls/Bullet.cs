using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourWithPause{

    Rigidbody rb;
    public float damage { get; set; }
    public float range { get; set; }
    public float speed { get; set; }

    Vector3 startPosition;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void Start(){
        MusicHandler.musicHandler.AddMusicIntensity(MusicHandler.musicHandler._gunShotIntensity);
    }

    protected override void UpdateWithPause(){
        if ((transform.position - startPosition).magnitude > range) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision){

        Debug.Log(collision.gameObject);

        HpComponent hp = collision.gameObject.GetComponent<HpComponent>();

        if (hp) {
            hp.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    public void AddSpeed(Vector3 direction) {
        if (rb.velocity.magnitude == 0) {
            rb.AddForce(direction * speed, ForceMode.VelocityChange);
        }
    }
}
