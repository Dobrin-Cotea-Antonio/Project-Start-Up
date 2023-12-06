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
    protected float cooldownBase;

    protected bool isOnCooldown = false;

    public bool isActive { get; protected set; }
    public Sprite _image { get ; protected set; }

    public Action<float> OnAbilityStart;
    public Action<float> OnAbilityEnd;

    protected Coroutine cooldownReset;
    protected PlayerStatsData data;

    protected virtual void Awake(){
        _image = image;
        cooldownBase = cooldown;
        data = GetComponent<PlayerStatsData>();

        Debug.Log(data);

        //Debug.Log(cooldownBase);
    }

    protected override void UpdateWithPause(){
        cooldown = cooldownBase * data.abilityRechargeSpeedMultiplier;
    }

    public abstract void UseAbility();

    protected void StartResetCooldown() {
        cooldownReset=StartCoroutine(CooldownTimer());
    }

    public void ResetCooldown() {
        isOnCooldown = false;
        if (cooldownReset!=null)
            StopCoroutine(cooldownReset);
    }

    IEnumerator CooldownTimer() {

        yield return new WaitForSecondsRealtime(cooldown);
        isOnCooldown = false;

    }
}
