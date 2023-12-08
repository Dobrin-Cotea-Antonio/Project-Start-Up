using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviourWithPause{

    [SerializeField] PlayerUIManager UIManager;

    InputManager input;
    bool wasInteractKeyPressed = false;
    bool wasInteractKeyPressedSecondary = false;
    bool isColliding = false;

    void Start(){
        input = GameObject.FindAnyObjectByType<InputManager>();
    }

    protected override void UpdateWithPause(){
        if (input.interactionInput&&isColliding) {
            wasInteractKeyPressed = true;
        }

        if (input.interactionInput2 && isColliding)
            wasInteractKeyPressedSecondary = true;


    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Interactable")) {
            InteractableData data = other.GetComponent<InteractableData>();

            if (!data){
                isColliding = true;
                return;
            }

            if (data.UI != null)
                data.UI.SetActive(true);

            if (data.isShopItem){

                if (data.objectType is AbilityPickUp){
                    UIManager.SetAbilityPickUpText("Buy", data.pickUpText, data.levelCost);
                    UIManager.EnableAbilityPickUpPrompt(true);
                } else {
                    UIManager.SetPickUpText("Buy", data.pickUpText, data.levelCost);
                    UIManager.EnablePickUpPrompt(true);
                }
            }
            else {
                if (data.objectType is AbilityPickUp){
                    UIManager.SetAbilityPickUpText(data.pickUpText);
                    UIManager.EnableAbilityPickUpPrompt(true);
                }
                else {
                    UIManager.SetPickUpText(data.pickUpText);
                    UIManager.EnablePickUpPrompt(true);
                }
            }

            if (data.objectType is Limb){
                UIManager.ToggleLimbComparison(true, LimbManager.limbManager.GetLimb(((Limb)data.objectType).limbType), ((Limb)data.objectType));
            }


            isColliding = true;
        }
    }

    private void OnTriggerStay(Collider other){
        if (wasInteractKeyPressed && other.CompareTag("Interactable")) {
            InteractableData data = other.GetComponent<InteractableData>();

            if (data.objectType is AbilityPickUp){
                ((AbilityPickUp)data.objectType).PickUp(1);
                wasInteractKeyPressed = false;
            }
            else {
                //other.gameObject.GetComponent<InteractableData>().PickUp();
                data.PickUp();
                if (data.objectType is Limb)
                    UIManager.ToggleLimbComparison(true, LimbManager.limbManager.GetLimb(((Limb)data.objectType).limbType), ((Limb)data.objectType));
                wasInteractKeyPressed = false;
            }

            wasInteractKeyPressed = false;

        }

        if (wasInteractKeyPressedSecondary && other.CompareTag("Interactable")) {
            InteractableData data = other.GetComponent<InteractableData>();

            if (data.objectType is AbilityPickUp) {
                ((AbilityPickUp)data.objectType).PickUp(2);
            }

            wasInteractKeyPressedSecondary = false;
        }

    }

    private void OnTriggerExit(Collider other){
        InteractableData data= other.GetComponent<InteractableData>();
        if (data) {
            if (data.UI!=null)
                data.UI.SetActive(false);
            if (data.objectType is AbilityPickUp)
                UIManager.EnableAbilityPickUpPrompt(false);
            if (data.objectType is Limb)
                UIManager.ToggleLimbComparison(false, LimbManager.limbManager.GetLimb(((Limb)data.objectType).limbType), ((Limb)data.objectType));
        }
        UIManager.EnablePickUpPrompt(false);

        isColliding = false;
    }
}
