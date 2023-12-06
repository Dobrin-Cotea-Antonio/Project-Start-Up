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

public class Limb : MonoBehaviourWithPause {
    [Header("Limb Data")]
    [SerializeField] string _limbName;
    [SerializeField] protected LimbTypes _limbType;
    [SerializeField] protected LimbData[] _limbData;

    public string limbName { get { return _limbName; } protected set { _limbName = value; } }
    public LimbTypes limbType { get { return _limbType; } protected set { _limbType = value; } }
    public LimbData[] limbData { get { return _limbData; } protected set { _limbData = value; } }

    public Sprite sprite { get; protected set; }
    public GameObject prefab { get; protected set; }
    public string itemName { get; protected set; }


    protected PlayerStatsData data;
    protected PlayerControls player;


    protected virtual void Start(){

        limbType = _limbType;
        data = GetComponent<PlayerStatsData>();
        player = GetComponent<PlayerControls>();

        if (data != null)
            AddBonuses(true);

    }

    protected void AddBonuses(bool pAdd){//true adds the values;false removes them 

        foreach (LimbData bonus in _limbData){
            switch (bonus.limbBonusType){
                case LimbBonuses.movementSpeed:
                    if (pAdd)
                        data.AddMovementModifier(limbType.ToString(), bonus.limbBonusValue);
                    else
                        data.AddMovementModifier(limbType.ToString(), 0);
                    break;
                case LimbBonuses.attackSpeed:
                    if (pAdd)
                        data.AddShootSpeedModifier(limbType.ToString(), bonus.limbBonusValue);
                    else
                        data.AddShootSpeedModifier(limbType.ToString(), 0);
                    break;
                case LimbBonuses.abilitiesCooldown:
                    if (pAdd)
                        data.AddAbilityRechargeSpeedModifier(limbType.ToString(), bonus.limbBonusValue);
                    else
                        data.AddAbilityRechargeSpeedModifier(limbType.ToString(), 0);
                    break;
                case LimbBonuses.dashCharges:
                    if (pAdd)
                        player.AddMaxDashCharges((int)bonus.limbBonusValue);
                    else
                        player.AddMaxDashCharges(-(int)bonus.limbBonusValue);
                    break;
                case LimbBonuses.bulletSpeed:
                    if (pAdd)
                        data.AddBulletSpeedModifier(limbType.ToString(), bonus.limbBonusValue);
                    else
                        data.AddBulletSpeedModifier(limbType.ToString(), 0);
                    break;

            }

        }

    }

    public void SetLimbData(LimbData[] pLimbData) {
        if (data != null)
            AddBonuses(false);
        limbData = pLimbData;
        if (data != null)
            AddBonuses(true);

    }

}
