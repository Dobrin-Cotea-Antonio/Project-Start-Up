using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubbleAbility : Ability{

    [Header("Data")]
    [SerializeField] GameObject shieldPrefab;

    public override void UseAbility(){
        if (!isOnCooldown) 
            StartCoroutine(CreateShield());
    }

    IEnumerator CreateShield() {
        isActive = true;
        isOnCooldown = true;

        GameObject g = Instantiate(shieldPrefab, transform);
        OnAbilityStart!.Invoke(abilityDuration);
        yield return new WaitForSeconds(abilityDuration);
        ResetCooldown();
        OnAbilityEnd!.Invoke(cooldown);

        isActive = false;
        Destroy(g);

    }
}
