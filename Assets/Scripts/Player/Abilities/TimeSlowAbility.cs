using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowAbility : Ability{

    [Header("Data")]
    [SerializeField] float timeSlowPercentage;

    PlayerStatsData data;

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

        data.AddShootSpeedModifier("SlowTimeBonus", timeSlowPercentage);
        data.AddMovementModifier("SlowTimeBonus", 10000f / (100 - timeSlowPercentage) - 100);
        data.AddDashSpeedModifier("SlowTimeBonus", 10000f / (100 - timeSlowPercentage) - 100);
        data.AddBulletSpeedModifier("SlowTimeBonus", 10000f / (100 - timeSlowPercentage) - 100);

        Time.timeScale = (1 - timeSlowPercentage / 100);
        OnAbilityStart!.Invoke(abilityDuration);
        yield return new WaitForSecondsRealtime(abilityDuration);
        OnAbilityEnd!.Invoke(cooldown);
        StartResetCooldown();

        Time.timeScale = 1;
        data.AddMovementModifier("SlowTimeBonus", 0);
        data.AddShootSpeedModifier("SlowTimeBonus", 0);
        data.AddDashSpeedModifier("SlowTimeBonus", 0);
        data.AddBulletSpeedModifier("SlowTimeBonus", 0);

        isActive = false;

    }
}
