using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbManager : MonoBehaviourWithPause{

    [SerializeField] Limb leftArm;
    [SerializeField] Limb rightArm;
    [SerializeField] Limb leftLeg;
    [SerializeField] Limb rightLeg;


    public void AddLimb(Limb pLimb) {
        switch (pLimb.limbType) {
            case LimbTypes.leftArm:
                leftArm.SetLimbData(pLimb.limbData);
                break;
            case LimbTypes.leftLeg:

                break;
            case LimbTypes.rightArm:

                break;
            case LimbTypes.rightLeg:

                break;
        
        }
    }
}
