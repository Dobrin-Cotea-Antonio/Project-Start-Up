using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ability : MonoBehaviourWithPause{

    [Header("Ability Info")]
    [SerializeField] protected float abilityDuration;
    [SerializeField] protected float cooldown;

    protected float lastTimeUsed = -10000;

    public abstract void UseAbility();
}
