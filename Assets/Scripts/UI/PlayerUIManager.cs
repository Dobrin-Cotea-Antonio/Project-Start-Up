using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIManager : MonoBehaviourWithPause{

    [Header("Categories")]
    [SerializeField] GameObject interactionTextAbilities;
    [SerializeField] GameObject interactionText;
    [Header("InputText")]
    [SerializeField] TextMeshProUGUI ability1Input;
    [SerializeField] TextMeshProUGUI ability2Input;
    [SerializeField] TextMeshProUGUI interactionInput;
    [Header("UpdateText")]
    [SerializeField] TextMeshProUGUI levelCash;
    [SerializeField] TextMeshProUGUI pickUpText;
    [SerializeField] TextMeshProUGUI ability1Text;
    [SerializeField] TextMeshProUGUI ability2Text;


    Controls controls;
    GameManager gameManager;

    private void Start(){
        controls = Controls.controls;
        gameManager = GameManager.gameManager;
    }

    protected override void UpdateWithPause(){
        ability1Input.text = string.Format("[{0}]", controls.keyList["interact"]);
        ability2Input.text = string.Format("[{0}]", controls.keyList["interact2"]);
        interactionInput.text = string.Format("[{0}]",controls.keyList["interact"]);
        levelCash.text = string.Format("{0}$",gameManager.levelCash);
    }

    public void EnableAbilityPickUpPrompt(bool pState) {
        interactionTextAbilities.SetActive(pState);
    }

    public void EnablePickUpPrompt(bool pState) {
        interactionText.SetActive(pState);
    }

    public void SetPickUpText(string pText) {
        pickUpText.text = pText;
    }

    public void SetAbilityPickUpText(string pText1,string pText2="",int pCost=0) {
        string text=pText1;
        if (pCost != 0) {
            text = text + " "+pCost;
        }
        if (pText2 != "") {
            text = text + "\n" + pText2;
        }

        ability1Text.text = (text + " 1");
        ability2Text.text = (text + " 2");
    }


}
