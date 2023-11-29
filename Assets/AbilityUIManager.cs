using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIManager : MonoBehaviourWithPause{

    [SerializeField] RawImage ability1UI;
    [SerializeField] RawImage ability2UI;

    public void SetAbility1Texture(Texture2D pTexture) {
        ability1UI.enabled = true;
        ability1UI.texture = pTexture;
    }

    public void SetAbility2Texture(Texture2D pTexture){
        ability2UI.enabled = true;
        ability2UI.texture = pTexture;
    }

    public void DeactivateAbility1Icon() {
        ability1UI.enabled = false;
    }

    public void DeactivateAbility2Icon() {
        ability2UI.enabled = false;
    }

}
