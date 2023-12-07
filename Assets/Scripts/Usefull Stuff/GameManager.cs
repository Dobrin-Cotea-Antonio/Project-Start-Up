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


    public List<LimbData[]> limbsData = new List<LimbData[]>();
    public string[] limbNames=new string[4] {"","","",""};
    public GameObject[] limbPrefabs=new GameObject[4] {null,null,null,null}; 


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
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
            limbsData = new List<LimbData[]>();
            limbsData.Add(null);
            limbsData.Add(null);
            limbsData.Add(null);
            limbsData.Add(null);
            limbNames = new string[4] { "", "", "", "" };
            limbPrefabs = new GameObject[4] { null, null, null, null };


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
