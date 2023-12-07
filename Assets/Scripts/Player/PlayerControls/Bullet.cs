using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviourWithPause{

    Rigidbody rb;
    public float damage { get; set; }
    public float range { get; set; }
    public float speed { get; set; }

    public AudioClip shotSoundClip;
    public AudioClip hitSoundClip;
    public AudioMixerGroup AudioMixer;

    Vector3 startPosition;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void PlaySFX(AudioClip audioClip, AudioMixerGroup audioMixerMaster, float audioVolume, float lifeTime)
    {
        GameObject audioObject = new GameObject();

        AudioSource currentAudioSource = audioObject.AddComponent<AudioSource>();
        currentAudioSource.clip = audioClip;
        currentAudioSource.outputAudioMixerGroup = audioMixerMaster;
        currentAudioSource.volume = audioVolume;
        currentAudioSource.pitch = Random.Range(0.8f, 1.2f);
        currentAudioSource.Play();
        Destroy(audioObject, 2);
    }

    private void Start(){
        MusicHandler.musicHandler.AddMusicIntensity(MusicHandler.musicHandler._gunShotIntensity);

        //GameObject audioObject = new GameObject();

        //AudioSource currentAudioSource = audioObject.AddComponent<AudioSource>();
        //currentAudioSource.clip = shotSoundClip;
        //currentAudioSource.outputAudioMixerGroup = AudioMixer;
        //currentAudioSource.volume = 0.5f;
        //currentAudioSource.pitch = Random.Range(0.8f, 1.2f);
        //currentAudioSource.Play();
        //Destroy(audioObject, 2);

        PlaySFX(shotSoundClip, AudioMixer, 0.5f, 2);
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
            PlaySFX(hitSoundClip, AudioMixer, 0.38f, 1);
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
