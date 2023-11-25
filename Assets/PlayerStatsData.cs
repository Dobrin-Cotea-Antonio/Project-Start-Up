using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsData : MonoBehaviourWithPause{
   
    public float movementSpeedMultiplier { get; private set; }
    public float shootSpeedMultiplier { get; private set; }

    //List<float> movementModifiers=new List<float>();
    Dictionary<string,float> movementSpeedModifiers=new Dictionary<string, float>();

    Dictionary<string, float> shootSpeedModifiers = new Dictionary<string, float>();

    private void Awake(){
        movementSpeedMultiplier = 1.0f;
        shootSpeedMultiplier = 1.0f;
    }

    public void AddMovementModifier(string pString,float pModif) {//pModif is a percentage
        if (movementSpeedModifiers.ContainsKey(pString)) {
            movementSpeedMultiplier = movementSpeedMultiplier - movementSpeedModifiers[pString] / 100;
        }

        movementSpeedModifiers[pString] = pModif;

        movementSpeedMultiplier = movementSpeedMultiplier + pModif / 100;

        //Debug.Log(movementSpeedMultiplier);
        //movementSpeedMultiplier = Mathf.Max(0, movementSpeedMultiplier + pModif/100);

    }

    public void AddShootSpeedModifier(string pString, float pModif) {
        if (shootSpeedModifiers.ContainsKey(pString)){
            shootSpeedMultiplier = shootSpeedMultiplier + shootSpeedModifiers[pString] / 100;
        }

        shootSpeedModifiers[pString] = pModif;

        shootSpeedMultiplier = shootSpeedMultiplier - pModif / 100;

        Debug.Log(shootSpeedMultiplier);
        //shootSpeedMultiplier = Mathf.Max(0.1f, shootSpeedMultiplier - pModif/100);
    }

}
