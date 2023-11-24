using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviourWithPause{

    [SerializeField] Ability[] abilities;
    [SerializeField] int activeAbilityIndex;

    InputManager input;

    void Start(){
        input = GetComponent<InputManager>();
    }

    
    protected override void UpdateWithPause(){
        if (input.skillInput) {
            abilities[activeAbilityIndex].UseAbility();
        }
    }
}
