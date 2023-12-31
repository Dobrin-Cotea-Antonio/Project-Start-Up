using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviourWithPause{

    public float _explosionIntensity { get { return explosionIntensity; } private set { explosionIntensity = value; } }
    public float _gunShotIntensity { get { return gunShotIntensity; } private set { gunShotIntensity = value; } }
    public float _enemyDeathIntensity { get { return enemyDeathIntensity; } private set { enemyDeathIntensity = value; } }

    public static MusicHandler musicHandler { get; private set; }

    [SerializeField] [Range(0.0f, 1.25f)] float musicTransitionProgress;
    public float _musicTransitionProgress { get { return musicTransitionProgress; } private set { musicTransitionProgress = value; } }

    [SerializeField] private AudioClip[] musicTracks;
    [SerializeField] private float transitionTimeSpeed = 1.0f;
    [SerializeField] private float targetVolume = 0.3f;

    [SerializeField] float tickDownPerSec;

    [SerializeField] float explosionIntensity;
    [SerializeField] float gunShotIntensity;
    [SerializeField] float enemyDeathIntensity;

    private List<AudioSource> audioSources;//1 to 10000


    private void Awake(){
        if (musicHandler != null)
        {
            Destroy(transform.root.gameObject);
        }
        else {
            musicHandler = this;
        }
    }

    // Start is called before the first frame update
    void Start(){

        GameManager.gameManager.musicHandler = this;
        //Instantiate audioSources List
        audioSources = new List<AudioSource>();

        //For loop used instead of foreach (because we need the index)
        for (int i = 0; i < musicTracks.Length; i++){
            AudioClip currentAudioClip = musicTracks[i];

            //Instiating new empty GameObject (This will hold the Audio Sources for each track)
            GameObject audioObject = new GameObject();
            audioObject.name = "MusicProgress" + i.ToString();

            //Adding AudioSource component to audioObject, and setting up properties
            AudioSource currentAudioSource = audioObject.AddComponent<AudioSource>();
            currentAudioSource.clip = currentAudioClip;
            currentAudioSource.loop = true;
            currentAudioSource.Play();
            currentAudioSource.volume = 0;

            //Add currentAudioSource to audioSources List
            audioSources.Add(currentAudioSource);

            //Add created audioObject as a child of the transform this script is attached to
            audioObject.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update(){

        musicTransitionProgress = Mathf.Max(0 ,musicTransitionProgress-tickDownPerSec/10000*Time.deltaTime);

        //Not very optimal to loop through the audioSources list every frame, but this works for now.
        //Optimize later if possible 
        //for (int i = 0; i < audioSources.Count; i++){

        //    AudioSource currentAudioSource = audioSources[i];

        //    float startFactor = (float)(i) / audioSources.Count;
        //    float endFactor = (float)(i + 1) / audioSources.Count;

        //    if (musicTransitionProgress >= startFactor && musicTransitionProgress <= endFactor) {
        //        currentAudioSource.volume = Mathf.Lerp(currentAudioSource.volume, targetVolume, Time.deltaTime * transitionTimeSpeed);
        //    } else {
        //        currentAudioSource.volume = Mathf.Lerp(currentAudioSource.volume, 0.0f, Time.deltaTime * transitionTimeSpeed);
        //    }
        //}

        audioSources[1].volume = musicTransitionProgress * targetVolume;
        audioSources[0].volume = targetVolume - Mathf.Min(musicTransitionProgress, 1) * targetVolume;
    }

    //between 1 and 10000 / 10000 is 1 in music transition progress
    public void AddMusicIntensity(float pIntensity) {
        musicTransitionProgress += pIntensity/10000;

        if (musicTransitionProgress > 1)
            musicTransitionProgress = 1;

        if (musicTransitionProgress < 0)
            musicTransitionProgress = 0;
    }
}
