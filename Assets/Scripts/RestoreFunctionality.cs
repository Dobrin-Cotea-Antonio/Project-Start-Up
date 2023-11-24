using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreFunctionality : MonoBehaviourWithPause{

    [Header("Data")]
    [SerializeField] float hpRestored;

    InteractableData data;

    void Start(){
        data = GetComponent<InteractableData>();
        data.OnPickUp += RestoreHp;
    }

    void RestoreHp() {
        GameManager.gameManager.player.GetComponent<HpComponent>().RestoreHp(hpRestored);
    }

}
