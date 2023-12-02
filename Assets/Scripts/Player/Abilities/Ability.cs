using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

abstract public class Ability : MonoBehaviourWithPause {

    [Header("Ability Info")]
    [SerializeField] Sprite image;
    [SerializeField] protected float abilityDuration;
    [SerializeField] protected float cooldown;

    protected bool isOnCooldown = false;

    public bool isActive { get; protected set; }
    public Sprite _image { get ; protected set; }

    public Action<float> OnAbilityStart;
    public Action<float> OnAbilityEnd;

    protected virtual void Awake(){
        _image = image;
    }

    public abstract void UseAbility();

    protected void ResetCooldown() {
        StartCoroutine(CooldownTimer());
    }

    IEnumerator CooldownTimer() {

        yield return new WaitForSecondsRealtime(cooldown);
        isOnCooldown = false;

    }
}
