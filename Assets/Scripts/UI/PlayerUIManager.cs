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

    [Header("LimbUI")]
    [SerializeField] GameObject limbHolder;

    [Header("Limb Equiped")]
    [SerializeField] TextMeshProUGUI limbNameEquiped;
    [SerializeField] TextMeshProUGUI limbMovementEquiped;
    [SerializeField] TextMeshProUGUI limbAttackEquiped;
    [SerializeField] TextMeshProUGUI limbDashEquiped;
    [SerializeField] TextMeshProUGUI limbBulletEquiped;
    [SerializeField] TextMeshProUGUI limbAbilitiesEquiped;

    [Header("Limb Pickup")]
    [SerializeField] TextMeshProUGUI limbNamePickup;
    [SerializeField] TextMeshProUGUI limbMovementPickup;
    [SerializeField] TextMeshProUGUI limbAttackPickup;
    [SerializeField] TextMeshProUGUI limbDashPickup;
    [SerializeField] TextMeshProUGUI limbBulletPickup;
    [SerializeField] TextMeshProUGUI limbAbilitiesPickup;

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
    }

    protected override void UpdateWithPause(){
        ability1Input.text = string.Format("[{0}]", controls.keyList["interact"]);
        ability2Input.text = string.Format("[{0}]", controls.keyList["interact2"]);
        interactionInput.text = string.Format("[{0}]",controls.keyList["interact"]);
        levelCash.text = string.Format("{0}",gameManager.levelCash);

        UpdateDashCharges();

    }

    public void EnableAbilityPickUpPrompt(bool pState) {
        interactionTextAbilities.SetActive(pState);
    }

    public void EnablePickUpPrompt(bool pState) {
        interactionText.SetActive(pState);
    }

    public void SetPickUpText(string pText,string pText2="",int pCost=0) {

        string text = pText;

        if (pCost != 0)
            text = text + " " + pCost;

        if (pText2 != "")
            text = text + "\n" + pText2;

        pickUpText.text = text;
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

        for (int i = player._dashChargesMax; i < 4; i++) {
            dashSlots[i].SetActive(false);
        }
    }

    public void ToggleLimbComparison(bool pState,Limb pLimbEquiped,Limb pLimbPickup) {
        limbHolder.SetActive(pState);

        limbNameEquiped.text = pLimbEquiped.itemName;
        limbNamePickup.text = pLimbPickup.itemName;

        SetTextBonuses(pLimbEquiped, pLimbPickup, limbMovementEquiped, limbMovementPickup, LimbBonuses.movementSpeed,"Movement Speed: +");
        SetTextBonuses(pLimbEquiped, pLimbPickup, limbAttackEquiped, limbAttackPickup, LimbBonuses.attackSpeed, "Attack Speed: +");
        SetTextBonuses(pLimbEquiped, pLimbPickup, limbDashEquiped, limbDashPickup, LimbBonuses.dashCharges, "Dash Charges: +");
        SetTextBonuses(pLimbEquiped, pLimbPickup, limbBulletEquiped, limbBulletPickup, LimbBonuses.bulletSpeed, "Bullet Speed: +");
        SetTextBonuses(pLimbEquiped, pLimbPickup, limbAbilitiesEquiped, limbAbilitiesPickup, LimbBonuses.abilitiesCooldown, "Ability Cooldown: +");

    }

    void SetTextBonuses(Limb pLimbEquiped, Limb pLimbPickup,TextMeshProUGUI pEquipedText,TextMeshProUGUI pPickupText,LimbBonuses pBonusType,string pText) {

        float bonusEquiped = pLimbEquiped.limbDataDictionary[pBonusType];
        float bonusPickup = pLimbPickup.limbDataDictionary[pBonusType];

        pEquipedText.text = pText + bonusEquiped;
        pPickupText.text = pText + bonusPickup;

        if (pBonusType != LimbBonuses.dashCharges) {
            pEquipedText.text += "%";
            pPickupText.text += "%";
        }

        Debug.Log(pEquipedText.text);
        Debug.Log(pPickupText.text);

        if (bonusEquiped == bonusPickup) {
            pEquipedText.color = Color.gray;
            pPickupText.color = Color.gray;
            return;
        }

        if (bonusEquiped > bonusPickup) {
            pEquipedText.color = Color.green;
            pPickupText.color = Color.red;
            return;
        }

        pEquipedText.color = Color.red;
        pPickupText.color = Color.green;
    }

}
