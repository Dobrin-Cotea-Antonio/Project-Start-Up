using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[CreateAssetMenu(fileName = "Data", menuName = "BaseInteractableData")]
public class InteractableData : MonoBehaviourWithPause{

    public Action OnPickUp;

    [Header("")]
    public GameObject UI;
    public TextMeshProUGUI UItext;
    public ItemPickUp objectType;
    public string popUpText;
    public string pickUpText;
    public int levelCost;
    public int hubCost;
    public bool isShopItem;


    private void Start(){
        //Debug.Log(popUpText);
        SetUIText(popUpText);
    }

    public void PickUp() {
        OnPickUp?.Invoke();
    }

    public void SetUIText(string pText) {
        UItext.text = pText;
    }


}
