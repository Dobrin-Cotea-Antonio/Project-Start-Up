using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDrops : MonoBehaviourWithPause{

    [Header("Drop Chances")]
    [SerializeField] int restoreDropChance;
    [SerializeField] int nothingChance;

    [Header("Objects")]
    [SerializeField] GameObject hpRestore;



    HpComponent hpComp;

    void Start(){
        hpComp = GetComponent<HpComponent>();
        hpComp.OnDeath += Drop;
    }


    void Drop() {

        int randomNumber = Random.Range(1,101);
        if (randomNumber <= restoreDropChance) {
            Instantiate(hpRestore, transform.position,transform.rotation);
        }
    }
}
