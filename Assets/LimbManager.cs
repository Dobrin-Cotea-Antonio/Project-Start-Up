using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbManager : MonoBehaviourWithPause{

    public static LimbManager limbManager;

    [SerializeField] Limb leftArm;
    [SerializeField] Limb rightArm;
    [SerializeField] Limb leftLeg;
    [SerializeField] Limb rightLeg;

    private void Awake(){
        if (limbManager!=null)
            Destroy(limbManager);
        limbManager = this;

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
        switch (pType){
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
}
