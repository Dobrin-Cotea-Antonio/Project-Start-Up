using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Interactable{

    [Header("Data")]
    [SerializeField] float hpRestored;

    InteractableData data;

    void Awake(){
        data = GetComponent<InteractableData>();
        data.OnPickUp += RestoreHp;
        data.popUpText =data.popUpText+ " " + hpRestored+" HP";
    }

    void RestoreHp() {
        if (GameManager.gameManager.levelCash < data.levelCost)
            return;

        GameManager.gameManager.levelCash -= data.levelCost;
        GameManager.gameManager.playerUIManager.EnablePickUpPrompt(false);
        GameManager.gameManager.playerHp.RestoreHp(hpRestored);
        Destroy(gameObject);
    }

}
