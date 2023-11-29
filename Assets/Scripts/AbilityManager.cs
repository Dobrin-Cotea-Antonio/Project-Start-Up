using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviourWithPause{

    [SerializeField] GameObject abilityDropPrefab;
    [SerializeField] AbilityUIManager abilityUIManager;
    [SerializeField] Ability[] abilities;
    [SerializeField] int activeAbilityIndex1;
    [SerializeField] int activeAbilityIndex2;

    string[] abilityText = { "Shield Ability", "Clone Ability", "Overdrive Ability", "Time Slow Ability" };

    InputManager input;

    void Start(){
        input = GetComponent<InputManager>();
        abilityUIManager.DeactivateAbility1Icon();
        abilityUIManager.DeactivateAbility2Icon();
    }


    protected override void UpdateWithPause(){

        if (activeAbilityIndex1!=-1 && input.ability1Input) {
            if (activeAbilityIndex2 == -1 || !abilities[activeAbilityIndex2].isActive)
                abilities[activeAbilityIndex1].UseAbility();
        }

        if (activeAbilityIndex2 != -1 && input.ability2Input) {
            if (activeAbilityIndex1==-1|| !abilities[activeAbilityIndex1].isActive)
            abilities[activeAbilityIndex2].UseAbility();
        }   
    }

    public void AddAbility(int pAbilityIndex,int pAbilitySlot) {

        if (pAbilitySlot == 1) {
            ChangeAbility1(pAbilityIndex);
        } else {
            ChangeAbility2(pAbilityIndex);
        }


    }

    void ChangeAbility1(int pAbilityIndex) {
        if (activeAbilityIndex1 != -1){
            GameObject g = Instantiate(abilityDropPrefab, transform.position, Quaternion.identity);
            AbilityPickUp ability = g.GetComponent<AbilityPickUp>();
            ability.SetData(activeAbilityIndex1, this, abilityText[activeAbilityIndex1]);

        }
        
        activeAbilityIndex1 = pAbilityIndex;
        abilityUIManager.SetAbility1Texture(abilities[activeAbilityIndex1]._image);

    }

    void ChangeAbility2(int pAbilityIndex) {
        if (activeAbilityIndex2 != -1){
            GameObject g = Instantiate(abilityDropPrefab, transform.position, Quaternion.identity);
            AbilityPickUp ability = g.GetComponent<AbilityPickUp>();
            ability.SetData(activeAbilityIndex2, this, abilityText[activeAbilityIndex2]);

        }


        activeAbilityIndex2 = pAbilityIndex;
        abilityUIManager.SetAbility2Texture(abilities[activeAbilityIndex2]._image);

    }

}
