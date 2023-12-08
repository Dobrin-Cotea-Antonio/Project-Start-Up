using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAndInvisAbility : Ability{

    [Header("Data")]
    [SerializeField] ChangeTransparency transparencyScript;
    [SerializeField] GameObject clonePrefab;
    [SerializeField] Transform modelRotation;
    [SerializeField] float cloneTransparency;

    private void Start(){
    }

    public override void UseAbility(){
        if (!isOnCooldown)
            StartCoroutine(CreateClone());
    }

    IEnumerator CreateClone() {
        isActive = true;
        isOnCooldown = true;
        
        GameObject clone = Instantiate(clonePrefab, GetComponent<PlayerControls>().GetModelHolder().transform.position, modelRotation.rotation);
        transparencyScript.SetTransparency();
        OnAbilityStart!.Invoke(abilityDuration);
        AnimateGlitch glitch = clone.GetComponent<AnimateGlitch>();


        EnemyManager.enemyManager.SetEnemyTarget(glitch.transform, glitch.GetRefPoint(), false);
        yield return new WaitForSecondsRealtime(abilityDuration);
        transparencyScript.SetTransparency();
        OnAbilityEnd!.Invoke(cooldown);
        StartResetCooldown();

        Destroy(clone);
        isActive = false;
    }

}