using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class Ability : MonoBehaviourWithPause {

    [Header("Ability Info")]
    [SerializeField] Texture2D image;
    [SerializeField] protected float abilityDuration;
    [SerializeField] protected float cooldown;

    protected float lastTimeUsed = -10000;

    public bool isActive { get; protected set; }
    public Texture2D _image { get ; protected set; }

    private void Awake(){
        _image = image;
    }

    public abstract void UseAbility();
}
