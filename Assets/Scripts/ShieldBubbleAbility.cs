using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubbleAbility : Ability{

    [Header("Data")]
    [SerializeField] GameObject shieldPrefab;

    public override void UseAbility(){
        if (Time.time - lastTimeUsed > cooldown) {
            lastTimeUsed = Time.time;
            StartCoroutine(CreateShield());
        }

    }

    IEnumerator CreateShield() {
        GameObject g = Instantiate(shieldPrefab, transform);
        isActive = true;

        yield return new WaitForSeconds(abilityDuration);

        isActive = false;
        Destroy(g);

    }
}
