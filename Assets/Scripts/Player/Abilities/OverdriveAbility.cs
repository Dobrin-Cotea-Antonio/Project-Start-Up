using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveAbility : Ability{
    
    [Header("Data")]
    [SerializeField] float movementSpeedBonus;
    [SerializeField] float shootSpeedBonus;
    [SerializeField] float hpRegenPerSec;
    [SerializeField] HpComponent hp;

    PlayerStatsData data;

    protected override void Awake(){
        base.Awake();
        GameManager.gameManager.playerHp = hp;
    }

    private void Start(){
        data = GetComponent<PlayerStatsData>();
    }

    public override void UseAbility(){
        if (!isOnCooldown)
            StartCoroutine(EnableAbility());
    }

    IEnumerator EnableAbility() {
        isActive = true;
        isOnCooldown = true;

        data.AddMovementModifier("OverdriveBonus", movementSpeedBonus);
        data.AddShootSpeedModifier("OverdriveBonus",shootSpeedBonus);
        StartCoroutine(RecoverHp());
        OnAbilityStart!.Invoke(abilityDuration);
        yield return new WaitForSecondsRealtime(abilityDuration);
        OnAbilityEnd!.Invoke(cooldown);
        StartResetCooldown();

        data.AddMovementModifier("OverdriveBonus", 0);
        data.AddShootSpeedModifier("OverdriveBonus",0);

        isActive = false;
    }

    IEnumerator RecoverHp() {
        int duration = (int)abilityDuration;

        for (int i = 0; i < duration; i++) {

            hp.RestoreHp(hpRegenPerSec);
            yield return new WaitForSecondsRealtime(1);
        
        }
    }
}
