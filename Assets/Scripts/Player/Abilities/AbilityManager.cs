using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviourWithPause{


    [SerializeField] GameObject[] abilitiesPrefabs;
    [SerializeField] Ability[] abilities;
    [SerializeField] AbilityUIManager abilityUIManager;
    [SerializeField] int activeAbilityIndex1;
    [SerializeField] int activeAbilityIndex2;

    InputManager input;

    void Start(){
        input = GetComponent<InputManager>();
        abilityUIManager.DeactivateAbility1Icon();
        abilityUIManager.DeactivateAbility2Icon();

        GameManager.gameManager.abilityManager = this;
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

    public bool AddAbility(int pAbilityIndex,int pAbilitySlot) {
        if (pAbilitySlot == 1) {
            if (activeAbilityIndex1 != -1 && abilities[activeAbilityIndex1].isActive)
                return false;
            ChangeAbility1(pAbilityIndex);
            return true;
        } else {
            if (activeAbilityIndex2 != -1 && abilities[activeAbilityIndex2].isActive)
                return false;
            ChangeAbility2(pAbilityIndex);
            return true;
        }

    }

    void ChangeAbility1(int pAbilityIndex) {
        if (activeAbilityIndex1 != -1) {
            abilities[activeAbilityIndex1].OnAbilityStart = null;
            abilities[activeAbilityIndex1].OnAbilityEnd = null;

            Instantiate(abilitiesPrefabs[activeAbilityIndex1], transform.position, Quaternion.identity);
        }


        abilityUIManager.ResetCooldown1();

        activeAbilityIndex1 = pAbilityIndex;
        abilityUIManager.SetAbility1Texture(abilities[activeAbilityIndex1]._image);

        abilities[activeAbilityIndex1].OnAbilityStart += abilityUIManager.FillCooldown1;
        abilities[activeAbilityIndex1].OnAbilityEnd += abilityUIManager.StartDecreasingCooldown1;

    }

    void ChangeAbility2(int pAbilityIndex) {
        if (activeAbilityIndex2 != -1) {
            abilities[activeAbilityIndex2].OnAbilityStart = null;
            abilities[activeAbilityIndex2].OnAbilityEnd = null;

            Instantiate(abilitiesPrefabs[activeAbilityIndex2], transform.position, Quaternion.identity);
        }

        abilityUIManager.ResetCooldown2();

        activeAbilityIndex2 = pAbilityIndex;
        abilityUIManager.SetAbility2Texture(abilities[activeAbilityIndex2]._image);

        abilities[activeAbilityIndex2].OnAbilityStart += abilityUIManager.FillCooldown2;
        abilities[activeAbilityIndex2].OnAbilityEnd += abilityUIManager.StartDecreasingCooldown2;

    }

}
