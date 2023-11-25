using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveAbility : Ability{
    
    [Header("Data")]
    [SerializeField] float movementSpeedBonus;
    [SerializeField] float shootSpeedBonus;
    [SerializeField] float hpRegenPerSec;

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

        data.AddMovementModifier("OverdriveBonus", movementSpeedBonus);
        data.AddShootSpeedModifier("OverdriveBonus",shootSpeedBonus);
        yield return new WaitForSeconds(abilityDuration);

        data.AddMovementModifier("OverdriveBonus", 0);
        data.AddShootSpeedModifier("OverdriveBonus",0);
        
    }
}
