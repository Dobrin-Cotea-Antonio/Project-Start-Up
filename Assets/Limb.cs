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
    public GameObject limbMeshHolder { get { return _limbMeshHolder; } protected set { _limbMeshHolder = value; } }
    public GameObject limbPrefab { get { return _limbPrefab; } protected set { _limbPrefab = value; } }

    public Dictionary<LimbBonuses,float> limbDataDictionary{ get; protected set;}

    [Header("Limb Data")]
    [SerializeField] string _limbName;
    [SerializeField] protected LimbTypes _limbType;
    [SerializeField] protected LimbData[] _limbData;
    [SerializeField] protected GameObject _limbMeshHolder;
    [SerializeField] protected GameObject _limbPrefab;

    [Header("Status")]
    [SerializeField] bool isPickUpObject;


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
        GameObject tempPrefab=limbPrefab;

        SetLimbData(pLimb.limbData, pLimb.limbName,pLimb.limbPrefab);
        pLimb.SetLimbData(tempLimbData, tempName, tempPrefab);
    }

    public void SetLimbData(LimbData[] pLimbData,string pName,GameObject pPrefab) {
        if (playerData != null)
            AddBonuses(false);

        limbName = pName;
        limbData = pLimbData;
        limbPrefab = pPrefab;

        if (isPickUpObject){
            interactData.popUpText = limbName;
            interactData.SetUIText(limbName);

            Destroy(limbMeshHolder.transform.GetChild(0).gameObject);
            GameObject arm = Instantiate(limbPrefab, limbMeshHolder.transform);
            arm.transform.localPosition = new Vector3(0, 0, 0);

        }

        ResetDictionary();

        if (playerData != null)
            AddBonuses(true);

    }

    void PickUp() {
        if (interactData.levelCost > GameManager.gameManager.levelCash)
            return;

        GameManager.gameManager.levelCash -= interactData.levelCost;
        interactData.levelCost = 0;
        interactData.isShopItem = false;
        GameManager.gameManager.playerUIManager.SetPickUpText(interactData.pickUpText);
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

    public void SetLimbsFromGameManager(LimbData[] pLimbData, string pName, GameObject pPrefab) {

        limbName = pName;
        limbData = pLimbData;
        limbPrefab = pPrefab;
        ResetDictionary();
        AddBonuses(true);

    }
}
