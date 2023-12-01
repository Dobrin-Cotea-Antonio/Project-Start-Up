using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickUp : ItemPickUp{

    [SerializeField] AbilityManager abilityManager;
    [SerializeField] int abilityIndex;

    InteractableData data;
    string pickUpText= "Equip Ability";

    private void Start(){
        data = GetComponent<InteractableData>();
        //data.OnPickUp += PickUp;
    }

    public int GetAbilityIndex() {
        return abilityIndex;
    }

    public void PickUp(int pAbilitySlot) {
        if (data.levelCost > GameManager.gameManager.levelCash)
            return;

        GameManager.gameManager.levelCash -= data.levelCost;

        abilityManager.AddAbility(abilityIndex, pAbilitySlot);
        Destroy(gameObject);
    }

    public void SetData(int pAbilityIndex, AbilityManager pAbilityManager,string pText) {
        data = GetComponent<InteractableData>();
        abilityIndex = pAbilityIndex;
        abilityManager = pAbilityManager;
        data.popUpText = pText;
        //data.SetUIText(pText);
        //data.UItext.text = "test";
    }
}
