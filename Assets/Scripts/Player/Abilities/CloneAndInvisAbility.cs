using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAndInvisAbility : Ability{

    [Header("Data")]
    [SerializeField] GameObject clonePrefab;
    [SerializeField] Transform modelRotation;
    [SerializeField] float cloneTransparency;

    public override void UseAbility(){
        if (!isOnCooldown)
            StartCoroutine(CreateClone());
    }

    IEnumerator CreateClone() {
        isActive = true;
        isOnCooldown = true;

        GameObject clone = Instantiate(clonePrefab, transform.position, modelRotation.rotation);
        clone.GetComponent<ChangeTransparency>().SetTransparency(cloneTransparency);
        OnAbilityStart!.Invoke(abilityDuration);
        yield return new WaitForSecondsRealtime(abilityDuration);
        OnAbilityEnd!.Invoke(cooldown);
        ResetCooldown();

        Destroy(clone);
        isActive = false;
    }

}