using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviourWithPause{
    [SerializeField] GameObject canvas;
    [SerializeField] RawImage hpImageBar;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] HpComponent hpComponent;

    private void Awake() {
        hpComponent.OnDamageTaken += UpdateHpBar;
    }

    void UpdateHpBar(float pCurrentHp,float pMaxHp) {
        float hpPercentage = pCurrentHp / pMaxHp;
        Debug.Log(hpPercentage * 100);

        text.text = string.Format("{0}% HP",System.Math.Round(hpPercentage * 100,2));
        hpImageBar.transform.localScale = new Vector2(hpPercentage, hpImageBar.transform.localScale.y);
    }


}
