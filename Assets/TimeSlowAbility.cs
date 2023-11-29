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
        if (Time.time - lastTimeUsed > cooldown){
            lastTimeUsed = Time.time;
            StartCoroutine(EnableAbility());
        }
    }

    IEnumerator EnableAbility() {
        isActive = true;

        data.AddShootSpeedModifier("SlowTimeBonus", timeSlowPercentage);
        data.AddMovementModifier("SlowTimeBonus",10000f/(100-timeSlowPercentage)-100);
        data.AddDashSpeedModifier("SlowTimeBonus", 10000f / (100 - timeSlowPercentage) - 100);

        Time.timeScale = (1 - timeSlowPercentage / 100);
        yield return new WaitForSecondsRealtime(abilityDuration);
        Time.timeScale = 1;
        data.AddMovementModifier("SlowTimeBonus", 0);
        data.AddShootSpeedModifier("SlowTimeBonus", 0);
        data.AddDashSpeedModifier("SlowTimeBonus", 0);

        isActive = false;

    }
}
