using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubbleAbility : Ability{

    [Header("Data")]
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] HpComponent playerHP;
    public override void UseAbility(){
        if (!isOnCooldown) 
            StartCoroutine(CreateShield());
    }

    IEnumerator CreateShield() {
        isActive = true;
        isOnCooldown = true;

        GameObject g = Instantiate(shieldPrefab, transform);
        playerHP.enabled = false;
        OnAbilityStart!.Invoke(abilityDuration);
        yield return new WaitForSeconds(abilityDuration);
        StartResetCooldown();
        playerHP.enabled = true;
        OnAbilityEnd!.Invoke(cooldown);

        isActive = false;
        Destroy(g);

    }
}
