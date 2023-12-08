using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : Interactable{

    public static int doorCount=0;

    [SerializeField] string targetScene;
    [SerializeField] SpriteRenderer spriteRenderer;

    InteractableData data;

    public int doorID { get; private set; }

    void Start(){
        data = GetComponent<InteractableData>();
        data.OnPickUp += Interact;
        gameObject.SetActive(false);
        doorID = doorCount++;
    }

    void Interact() {
        if (isActiveAndEnabled) {
            GameManager.gameManager.roomSelected = doorID;
            Debug.Log(GameManager.gameManager.roomSelected);
            SceneManager.LoadScene(targetScene);
        }

    }

    public void ActivateDoor(GameManager.RewardTypes pType) {

        gameObject.SetActive(true);
        spriteRenderer.gameObject.SetActive(true);
        switch (pType){
            case GameManager.RewardTypes.Ability:
                spriteRenderer.sprite = RoomManager.roomManager.sprites[2];
                break;
            case GameManager.RewardTypes.Hp:
                spriteRenderer.sprite=RoomManager.roomManager.sprites[0];
                break;
            case GameManager.RewardTypes.Limb:
                spriteRenderer.sprite = RoomManager.roomManager.sprites[1];
                spriteRenderer.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                break;
            case GameManager.RewardTypes.Shop:
                spriteRenderer.sprite = RoomManager.roomManager.sprites[3];
                break;


        }


    }


}
