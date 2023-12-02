using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : ItemPickUp{

    [Header("Data")]
    [SerializeField] float hpRestored;

    InteractableData data;

    void Awake(){
        data = GetComponent<InteractableData>();
        data.OnPickUp += RestoreHp;
        data.popUpText =data.popUpText+ " " + hpRestored+" HP";
    }

    void RestoreHp() {
        GameManager.gameManager.playerHp.RestoreHp(hpRestored);
    }

}
