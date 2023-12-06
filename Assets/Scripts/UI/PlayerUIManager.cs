using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("DashUI")]
    [SerializeField] GameObject[] dashSlots;
    [SerializeField] Sprite dashChargeFull;
    [SerializeField] Sprite dashChargeEmpty;

    List<Image> dashImages = new List<Image>();

    Controls controls;
    GameManager gameManager;
    PlayerControls player;

    private void Start(){
        controls = Controls.controls;
        gameManager = GameManager.gameManager;
        gameManager.playerUIManager = this;

        for (int i = 0; i < dashSlots.Length; i++)
            dashImages.Add(dashSlots[i].GetComponent<Image>());
            //dashImages[i] = dashSlots[i].GetComponent<Image>();
    }

    protected override void UpdateWithPause(){
        ability1Input.text = string.Format("[{0}]", controls.keyList["interact"]);
        ability2Input.text = string.Format("[{0}]", controls.keyList["interact2"]);
        interactionInput.text = string.Format("[{0}]",controls.keyList["interact"]);
        levelCash.text = string.Format("{0}$",gameManager.levelCash);

        UpdateDashCharges();

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

    void UpdateDashCharges() { 
        if (player == null)
            player = GameManager.gameManager.player.GetComponent<PlayerControls>();

        for (int i = 0; i < player._dashChargesMax; i++) {
            dashSlots[i].SetActive(true);
            if (i < player.availableDashCharges)
                dashImages[i].sprite = dashChargeFull;
            else
                dashImages[i].sprite = dashChargeEmpty;
        }
    }

}
