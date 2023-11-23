using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviourWithPause
{

    InputManager input;
    bool wasInteractKeyPressed = false;
    bool isColliding = false;

    void Start(){
        input = GameObject.FindAnyObjectByType<InputManager>();
    }

    protected override void UpdateWithPause(){
        if (input.interactionInput&&isColliding) {
            wasInteractKeyPressed = true;

        }

    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Interactable")) {
            other.GetComponent<InteractableData>().UI.SetActive(true);
            isColliding = true;
        }
    }

    private void OnTriggerStay(Collider other){
        if (wasInteractKeyPressed&& other.CompareTag("Interactable")) {
            Destroy(other.gameObject);
            wasInteractKeyPressed = false;
        }
    }

    private void OnTriggerExit(Collider other){
        InteractableData data= other.GetComponent<InteractableData>();
        if (data)
            data.UI.SetActive(false);
        isColliding = false;
    }
}
