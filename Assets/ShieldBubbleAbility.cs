using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubbleAbility : Ability{

    [Header("Data")]
    [SerializeField] GameObject shieldPrefab;
    



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void UseAbility(){
        if (Time.time - lastTimeUsed > cooldown) {
            lastTimeUsed = Time.time;
            StartCoroutine(CreateShield());
        }

    }

    IEnumerator CreateShield() {
        GameObject g = Instantiate(shieldPrefab, transform);

        yield return new WaitForSeconds(abilityDuration);

        Destroy(g);

    }
}
