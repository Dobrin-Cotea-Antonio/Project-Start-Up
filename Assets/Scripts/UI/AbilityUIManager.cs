using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AbilityUIManager : MonoBehaviourWithPause{

    [Header("Images")]
    [SerializeField] Image ability1UI;
    [SerializeField] Image ability2UI;

    [Header("Cooldown")]
    [SerializeField] Image cooldownImg1;
    [SerializeField] Image cooldownImg2;

    [Header("Colors")]
    [SerializeField] Color colorAbilityInUse;
    [SerializeField] Color colorCooldown;

    Tween tweenCooldown1;
    Tween tweenCooldown2;

    public void SetAbility1Texture(Sprite pTexture) {
        ability1UI.enabled = true;
        ability1UI.sprite = pTexture; 
    }

    public void SetAbility2Texture(Sprite pTexture){
        ability2UI.enabled = true;
        ability2UI.sprite = pTexture;
    }

    public void DeactivateAbility1Icon() {
        ability1UI.enabled = false;
    }

    public void DeactivateAbility2Icon() {
        ability2UI.enabled = false;
    }

    public void ResetCooldown1(){
        tweenCooldown1.Pause();
        cooldownImg1.fillAmount = 0;
    }

    public void FillCooldown1(float pDuration) {
        tweenCooldown1.Pause();
        cooldownImg1.color = colorAbilityInUse;
        cooldownImg1.fillAmount = 1;
        tweenCooldown1 = cooldownImg1.DOFillAmount(0, pDuration);
    }

    public void StartDecreasingCooldown1(float pDuration) {
        tweenCooldown1.Pause();
        cooldownImg1.color = colorCooldown;
        cooldownImg1.fillAmount = 1;
        tweenCooldown1 = cooldownImg1.DOFillAmount(0, pDuration);    
    }


    public void ResetCooldown2() {
        tweenCooldown2.Pause();
        cooldownImg2.fillAmount = 0;
    }

    public void FillCooldown2(float pDuration){
        tweenCooldown2.Pause();
        cooldownImg2.color = colorAbilityInUse;
        cooldownImg2.fillAmount = 1;
        tweenCooldown2 = cooldownImg2.DOFillAmount(0, pDuration);
        
    }

    public void StartDecreasingCooldown2(float pDuration){
        tweenCooldown2.Pause();
        cooldownImg2.color = colorCooldown;
        cooldownImg2.fillAmount = 1;
        tweenCooldown2 =cooldownImg2.DOFillAmount(0, pDuration);
    }

}
