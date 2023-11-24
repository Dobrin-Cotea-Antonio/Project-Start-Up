using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ability : MonoBehaviourWithPause{

    [Header("Ability Info")]
    [SerializeField] protected float abilityDuration;
    [SerializeField] protected float cooldown;

    protected float lastTimeUsed = -10000;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    protected override void UpdateWithPause()
    {
        
    }

    public abstract void UseAbility();
}
