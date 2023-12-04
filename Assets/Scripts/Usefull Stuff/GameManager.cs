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

    public enum RewardTypes { 
        Hp,
        Shop,
        Limb,
        Ability,
        HubCurrency,
        Grenades,
        None
    }

    public RewardTypes currentRoomReward { get; set; }
    public RewardTypes nextRoomReward { get; set; }


    private void Awake(){
        if (gameManager != null) {
            gameManager.currentRoomReward = nextRoomReward;
            gameManager.nextRoomReward = ((RewardTypes)Random.Range(0, 6));
            Debug.Log(gameManager.nextRoomReward);
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
            gameManager = this;
            gameManager.currentRoomReward = RewardTypes.Ability;
            gameManager.nextRoomReward = ((RewardTypes)Random.Range(0, 6));


        }
    }

    private void Start(){
        levelCash = 1000;
    }
}
