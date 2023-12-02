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

            if (data) {
                data.UI.SetActive(true);
                if (data.objectType is AbilityPickUp) {
                    if (data.isShopItem) {
                        UIManager.SetAbilityPickUpText("Buy",data.pickUpText,data.levelCost);
                    }
                    else {
                        UIManager.SetAbilityPickUpText(data.pickUpText);
                    }
                    UIManager.EnableAbilityPickUpPrompt(true);

                }

                else {

                    UIManager.SetPickUpText(data.pickUpText);
                    UIManager.EnablePickUpPrompt(true); 
                }
            }

            isColliding = true;
        }
    }

    private void OnTriggerStay(Collider other){
        if (wasInteractKeyPressed && other.CompareTag("Interactable")) {
            InteractableData data = other.GetComponent<InteractableData>();

            if (data.objectType is AbilityPickUp){
                UIManager.EnableAbilityPickUpPrompt(false);
                ((AbilityPickUp)data.objectType).PickUp(1);
                wasInteractKeyPressed = false;
            }
            else {
                other.gameObject.GetComponent<InteractableData>().PickUp();
                UIManager.EnablePickUpPrompt(false);
                wasInteractKeyPressed = false;
                Destroy(other.gameObject);
            }

            wasInteractKeyPressed = false;

        }

        if (wasInteractKeyPressedSecondary && other.CompareTag("Interactable")) {
            InteractableData data = other.GetComponent<InteractableData>();

            if (data.objectType is AbilityPickUp) {
                ((AbilityPickUp)data.objectType).PickUp(2);
                UIManager.EnableAbilityPickUpPrompt(false);
                //Destroy(other.gameObject);
            }

            wasInteractKeyPressedSecondary = false;
        }

    }

    private void OnTriggerExit(Collider other){
        InteractableData data= other.GetComponent<InteractableData>();
        if (data) {
            data.UI.SetActive(false);
            if (data.objectType is AbilityPickUp)
                UIManager.EnableAbilityPickUpPrompt(false);
        }
        UIManager.EnablePickUpPrompt(false);

        isColliding = false;
    }
}
