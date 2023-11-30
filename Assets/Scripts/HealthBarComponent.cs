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

    [SerializeField] bool dissapearAfterTime;
    [SerializeField] float timeToDissapear;

    float lastActiveTime=-1000000;
    bool enableCanvas = false;

    private void Awake() {
        hpComponent.OnDamageTaken += UpdateHpBar;
        
    }

    private void Start(){
        if (canvas == null)
            return;

        if (dissapearAfterTime)
            canvas.SetActive(false);
    }

    protected override void UpdateWithPause() {
        if (canvas == null)
            return;

        if (dissapearAfterTime && Time.time - lastActiveTime >= timeToDissapear)
            canvas.SetActive(false);
        
    }

    void UpdateHpBar(float pCurrentHp, float pMaxHp) {

        if (canvas == null)
            return;

        if (dissapearAfterTime && enableCanvas) {
            canvas.SetActive(true);
        }

        enableCanvas = true;

        lastActiveTime = Time.time;
        float hpPercentage = pCurrentHp / pMaxHp;

        text.text = string.Format("{0}% HP",System.Math.Round(hpPercentage * 100,2));
        hpImageBar.transform.localScale = new Vector2(hpPercentage, hpImageBar.transform.localScale.y);
    }

}
