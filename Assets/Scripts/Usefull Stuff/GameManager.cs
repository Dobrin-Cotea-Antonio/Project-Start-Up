using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourWithPause {
    public static GameManager gameManager { get; private set; }
    public static bool gameIsPaused { get; set; }
    public GameObject player { get; set; }
    public int permanentCash { get; set; }
    public int levelCash { get; set; }
    public AbilityManager abilityManager { get; set; }
    public HpComponent playerHp { get; set; }
    public PlayerUIManager playerUIManager { get ; set; }
    public MusicHandler musicHandler { get; set; }


    public int ability1 = -1;
    public int ability2 = -1;

    public bool abilitySet = false;

    public enum RewardTypes { 
        Hp,
        Shop,
        Limb,
        Ability,
        HubCurrency,
        None
    }

    public RewardTypes currentRoomReward { get; set; }
    public RewardTypes nextRoomReward { get; set; }


    private void Awake(){
        if (gameManager != null) {
            gameManager.currentRoomReward = nextRoomReward;
            gameManager.nextRoomReward = ((RewardTypes)Random.Range(0, 5));
            gameManager.abilitySet = false;
            //Debug.Log(gameManager.nextRoomReward);
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
            levelCash = 1000;
            gameManager = this;
            gameManager.currentRoomReward = RewardTypes.Ability;
            gameManager.nextRoomReward = ((RewardTypes)Random.Range(0, 5));


        }
    }

    private void Update(){

        if (!abilitySet){
            if (ability1 != -1)
                abilityManager.AddAbility(ability1, 1);
            if (ability2 != -1)
                abilityManager.AddAbility(ability2, 2);

            abilitySet = true;
        } else {
            ability1 = abilityManager._activeAbilityIndex1;
            ability2 = abilityManager._activeAbilityIndex2;
        }

    }

}
