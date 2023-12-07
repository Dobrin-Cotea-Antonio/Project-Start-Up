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
    public int roomSelected;

    public enum RewardTypes { 
        Hp,
        Limb,
        Ability,
        Shop,
        None
    }

    public RewardTypes currentRoomReward { get; set; }
    public List<RewardTypes> nextRoomRewards { get; set; }


    private void Awake(){
        if (gameManager != null) {

            gameManager.currentRoomReward = gameManager.nextRoomRewards[gameManager.roomSelected];  
            
            //roomSelected = -1;
            GenerateReward();
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
            roomSelected = -1;
            gameManager = this;
            currentRoomReward = RewardTypes.Ability;
            nextRoomRewards = new List<RewardTypes>();
            nextRoomRewards.Add(RewardTypes.None);
            nextRoomRewards.Add(RewardTypes.None);


            GenerateReward();



        }
    }

    void GenerateReward() {
        gameManager.nextRoomRewards[0] = ((RewardTypes)Random.Range(0, 3));
        gameManager.nextRoomRewards[1] = ((RewardTypes)Random.Range(0, 3));
        while (gameManager.nextRoomRewards[0] == gameManager.nextRoomRewards[1]) {
            gameManager.nextRoomRewards[1] = ((RewardTypes)Random.Range(0, 3));
        }

        Debug.Log(gameManager.nextRoomRewards[0] + " " + gameManager.nextRoomRewards[1]);
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
