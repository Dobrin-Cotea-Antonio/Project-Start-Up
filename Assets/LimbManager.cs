using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbManager : MonoBehaviourWithPause {

    public static LimbManager limbManager;

    [SerializeField] Limb leftArm;
    [SerializeField] Limb rightArm;
    [SerializeField] Limb leftLeg;
    [SerializeField] Limb rightLeg;

    bool setData = false;

    private void Awake() {
        if (limbManager != null)
            Destroy(limbManager);
        limbManager = this;

    }

    protected override void UpdateWithPause(){

        if (!setData){
            if (GameManager.gameManager.limbsData[0]!=null)
                GetDataFromGameManager();
            setData = true;
        } else {
            SetLimbDataToGameManager();
        }
    }

    public void AddLimb(Limb pLimb) {
        switch (pLimb.limbType) {
            case LimbTypes.leftArm:
                leftArm.SwapLimbData(pLimb);
                break;
            case LimbTypes.leftLeg:
                leftLeg.SwapLimbData(pLimb);
                break;
            case LimbTypes.rightArm:
                rightArm.SwapLimbData(pLimb);
                break;
            case LimbTypes.rightLeg:
                rightLeg.SwapLimbData(pLimb);
                break;

        }
    }

    public Limb GetLimb(LimbTypes pType) {
        switch (pType) {
            case LimbTypes.leftArm:
                return leftArm;
            case LimbTypes.leftLeg:
                return leftLeg;
            case LimbTypes.rightArm:
                return rightArm;
            case LimbTypes.rightLeg:
                return rightLeg;
        }
        return null;

    }

    public void SetLimbDataToGameManager() {

        GameManager gameManager = GameManager.gameManager;

        int i = 0;

        gameManager.limbsData[i] = leftArm.limbData;
        gameManager.limbNames[i] = leftArm.limbName;
        gameManager.limbPrefabs[i] = leftArm.limbPrefab;

        i++;

        gameManager.limbsData[i] = leftLeg.limbData;
        gameManager.limbNames[i] = leftLeg.limbName;
        gameManager.limbPrefabs[i] = leftLeg.limbPrefab;

        i++;

        gameManager.limbsData[i] = rightArm.limbData;
        gameManager.limbNames[i] = rightArm.limbName;
        gameManager.limbPrefabs[i] = rightArm.limbPrefab;

        i++;

        gameManager.limbsData[i] = rightLeg.limbData;
        gameManager.limbNames[i] = rightLeg.limbName;
        gameManager.limbPrefabs[i] = rightLeg.limbPrefab;

    }

    void GetDataFromGameManager() {

        GameManager gameManager = GameManager.gameManager;

        leftArm.SetLimbsFromGameManager(gameManager.limbsData[0], gameManager.limbNames[0], gameManager.limbPrefabs[0]);
        leftLeg.SetLimbsFromGameManager(gameManager.limbsData[1], gameManager.limbNames[0], gameManager.limbPrefabs[1]);
        rightArm.SetLimbsFromGameManager(gameManager.limbsData[2], gameManager.limbNames[0], gameManager.limbPrefabs[2]);
        rightLeg.SetLimbsFromGameManager(gameManager.limbsData[3], gameManager.limbNames[0], gameManager.limbPrefabs[3]);
    }
}
