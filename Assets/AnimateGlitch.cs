using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateGlitch: MonoBehaviour{

    [Header("Data")]
    [SerializeField] Material material;
    [SerializeField] float maxGlitch;
    [SerializeField] float minGlitch;
    [Header("Length")]
    [SerializeField] float glitchLengthMain;
    [SerializeField] float glitchLengthSecondary;
    [Header("Time")]
    [SerializeField] float timeBetweenGlitches;
    [SerializeField] float timeBetweenMainAndSec;

    private void Start(){
        StartCoroutine(Glitch());

    }

    IEnumerator Glitch(){

        while (true) {

            yield return new WaitForSeconds(timeBetweenGlitches+Random.Range(-1f,1f));

            material.SetFloat("_glitchStrength", Random.Range(minGlitch, maxGlitch));

            yield return new WaitForSeconds(glitchLengthMain + Random.Range(-0.1f, 0.1f));

            material.SetFloat("_glitchStrength", 0f);

            yield return new WaitForSeconds(timeBetweenMainAndSec + Random.Range(-0.05f, 0.05f));

            material.SetFloat("_glitchStrength", -Random.Range(minGlitch, maxGlitch));

            yield return new WaitForSeconds(glitchLengthSecondary + Random.Range(-0.05f, 0.05f));

            material.SetFloat("_glitchStrength", 0f);

        }
        
    }
}