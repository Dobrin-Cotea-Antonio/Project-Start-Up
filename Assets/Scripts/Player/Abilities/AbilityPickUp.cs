using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPickUp : Interactable{

    [SerializeField] int abilityIndex;
         
    InteractableData data;

    private void Start(){
        data = GetComponent<InteractableData>();
    }

    public int GetAbilityIndex() {
        return abilityIndex;
    }

    public void PickUp(int pAbilitySlot) {
        if (data.levelCost > GameManager.gameManager.levelCash)
            return;

        
        if (GameManager.gameManager.abilityManager.AddAbility(abilityIndex, pAbilitySlot)) {
            GameManager.gameManager.levelCash -= data.levelCost;
            GameManager.gameManager.playerUIManager.EnableAbilityPickUpPrompt(false);
            Destroy(gameObject);
        }

    }

}
