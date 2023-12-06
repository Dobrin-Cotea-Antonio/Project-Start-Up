using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : Interactable{

    static int doorCount=0;

    [SerializeField] string targetScene;

    InteractableData data;

    int doorID;

    void Start(){
        data = GetComponent<InteractableData>();
        data.OnPickUp += Interact;
        Debug.Log(gameObject);
        gameObject.SetActive(false);
        doorID = doorCount++;
    }

    void Interact() {
        if (isActiveAndEnabled)
            SceneManager.LoadScene(targetScene);
    }



}
