using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAndInvisAbility : Ability{

    [Header("Data")]
    [SerializeField] GameObject clonePrefab;
    [SerializeField] Transform modelRotation;
    [SerializeField] float cloneTransparency;


    public override void UseAbility(){

        if (Time.time - lastTimeUsed >= cooldown) {
            StartCoroutine(CreateClone());
        }

    }


    IEnumerator CreateClone() {
        isActive = true;
        lastTimeUsed = Time.time;
        GameObject clone = Instantiate(clonePrefab, transform.position, modelRotation.rotation);
        clone.GetComponent<ChangeTransparency>().SetTransparency(cloneTransparency);

        yield return new WaitForSeconds(abilityDuration);

        Destroy(clone);
        isActive = false;
    }

}