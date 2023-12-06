using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class LimbData {
    public LimbBonuses limbBonusType;
    public float limbBonusValue;
}

public enum LimbBonuses{
    movementSpeed,
    attackSpeed,
    dashCharges,
    bulletSpeed,
    abilitiesCooldown,
}

public enum LimbTypes{
    leftArm,
    rightArm,
    leftLeg,
    rightLeg
}

public class Limb : Interactable {

    public string limbName { get { return _limbName; } protected set { _limbName = value; } }
    public LimbTypes limbType { get { return _limbType; } protected set { _limbType = value; } }
    public LimbData[] limbData { get { return _limbData; } protected set { _limbData = value; } }
    public string itemName { get { return _limbName; } protected set { _limbName = value; } }

    public Dictionary<LimbBonuses,float> limbDataDictionary{ get; protected set;}

    [Header("Limb Data")]
    [SerializeField] string _limbName;
    [SerializeField] protected LimbTypes _limbType;
    [SerializeField] protected LimbData[] _limbData;

    [Header("Status")]
    [SerializeField] bool isPickUpObject;


    public Sprite sprite { get; protected set; }
    public GameObject prefab { get; protected set; }


    protected PlayerStatsData playerData;
    protected PlayerControls player;
    protected InteractableData interactData;

    bool initialSetup = false;


    protected virtual void Start(){

        limbType = _limbType;

        limbDataDictionary = new Dictionary<LimbBonuses, float>();

        ResetDictionary();
    }

    void Update(){


        if (!initialSetup){

            if (isPickUpObject){
                interactData = GetComponent<InteractableData>();
                interactData.OnPickUp += PickUp;
                interactData.popUpText = limbName;
                interactData.SetUIText(limbName);

            } else {
                playerData = GetComponent<PlayerStatsData>();
                player = GetComponent<PlayerControls>();
                AddBonuses(true);
            }

            initialSetup = true;
        }
    }

    protected void AddBonuses(bool pAdd){//true adds the values;false removes them 

        foreach (LimbData bonus in _limbData){
            switch (bonus.limbBonusType){
                case LimbBonuses.movementSpeed:
                    if (pAdd)
                        playerData.AddMovementModifier(limbType.ToString(), bonus.limbBonusValue);
                    else
                        playerData.AddMovementModifier(limbType.ToString(), 0);
                    break;
                case LimbBonuses.attackSpeed:
                    if (pAdd)
                        playerData.AddShootSpeedModifier(limbType.ToString(), bonus.limbBonusValue);
                    else
                        playerData.AddShootSpeedModifier(limbType.ToString(), 0);
                    break;
                case LimbBonuses.abilitiesCooldown:
                    if (pAdd)
                        playerData.AddAbilityRechargeSpeedModifier(limbType.ToString(), bonus.limbBonusValue);
                    else
                        playerData.AddAbilityRechargeSpeedModifier(limbType.ToString(), 0);
                    break;
                case LimbBonuses.dashCharges:
                    if (pAdd)
                        player.AddMaxDashCharges((int)bonus.limbBonusValue);
                    else
                        player.AddMaxDashCharges(-(int)bonus.limbBonusValue);
                    break;
                case LimbBonuses.bulletSpeed:
                    if (pAdd)
                        playerData.AddBulletSpeedModifier(limbType.ToString(), bonus.limbBonusValue);
                    else
                        playerData.AddBulletSpeedModifier(limbType.ToString(), 0);
                    break;

            }

        }

    }

    public void SwapLimbData(Limb pLimb) {

        LimbData[] tempLimbData = limbData;
        string tempName = limbName;

        SetLimbData(pLimb.limbData, pLimb.limbName);
        pLimb.SetLimbData(tempLimbData, tempName);
    }

    public void SetLimbData(LimbData[] pLimbData,string pName) {
        if (playerData != null)
            AddBonuses(false);

        limbName = pName;
        limbData = pLimbData;

        if (isPickUpObject){
            interactData.popUpText = limbName;
            interactData.SetUIText(limbName);
        }

        ResetDictionary();

        if (playerData != null)
            AddBonuses(true);

    }

    void PickUp() {
        if (interactData.levelCost > GameManager.gameManager.levelCash)
            return;


        //GameManager.gameManager.playerUIManager.EnablePickUpPrompt(false);
        GameManager.gameManager.levelCash -= interactData.levelCost;
        interactData.levelCost = 0;
        interactData.isShopItem = false;
        GameManager.gameManager.playerUIManager.SetPickUpText(interactData.pickUpText);
        //GameManager.gameManager.playerUIManager.EnablePickUpPrompt(true);
        LimbManager.limbManager.AddLimb(this);

    }

    void ResetDictionary() {
        limbDataDictionary[LimbBonuses.movementSpeed] = 0;
        limbDataDictionary[LimbBonuses.attackSpeed] = 0;
        limbDataDictionary[LimbBonuses.dashCharges] = 0;
        limbDataDictionary[LimbBonuses.bulletSpeed] = 0;
        limbDataDictionary[LimbBonuses.abilitiesCooldown] = 0;

        foreach (LimbData d in _limbData){
            limbDataDictionary[d.limbBonusType] = d.limbBonusValue;
        }
    }
}
