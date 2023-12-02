using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsData : MonoBehaviourWithPause{
   
    public float movementSpeedMultiplier { get; private set; }
    public float shootSpeedMultiplier { get; private set; }
    public float dashSpeedMultiplier { get; private set; }
    public float bulletSpeedMultiplier { get; private set; }

    //List<float> movementModifiers=new List<float>();
    Dictionary<string,float> movementSpeedModifiers=new Dictionary<string, float>();

    Dictionary<string, float> shootSpeedModifiers = new Dictionary<string, float>();

    Dictionary<string, float> dashSpeedModifiers = new Dictionary<string, float>();

    Dictionary<string, float> bulletSpeedModifiers = new Dictionary<string, float>();

    private void Awake(){
        movementSpeedMultiplier = 1.0f;
        shootSpeedMultiplier = 1.0f;
        dashSpeedMultiplier = 1.0f;
        bulletSpeedMultiplier = 1.0f;
    }

    public void AddMovementModifier(string pString,float pModif) {//pModif is a percentage
        if (movementSpeedModifiers.ContainsKey(pString)) {
            movementSpeedMultiplier = movementSpeedMultiplier - movementSpeedModifiers[pString] / 100;
        }

        movementSpeedModifiers[pString] = pModif;

        movementSpeedMultiplier = movementSpeedMultiplier + pModif / 100;

    }

    public void AddShootSpeedModifier(string pString, float pModif) {
        if (shootSpeedModifiers.ContainsKey(pString)){
            shootSpeedMultiplier = shootSpeedMultiplier - shootSpeedModifiers[pString] / 100;
        }

        shootSpeedModifiers[pString] = pModif;

        shootSpeedMultiplier = shootSpeedMultiplier + pModif / 100;

    }


    public void AddDashSpeedModifier(string pString,float pModif) {
        if (dashSpeedModifiers.ContainsKey(pString)) {
            dashSpeedMultiplier = dashSpeedMultiplier - dashSpeedModifiers[pString] / 100;
        }

        dashSpeedModifiers[pString] = pModif;

        dashSpeedMultiplier = dashSpeedMultiplier + pModif / 100;
    }

    public void AddBulletSpeedModifier(string pString, float pModif) {
        if (bulletSpeedModifiers.ContainsKey(pString)) { 
            
        }
    
    }

}
