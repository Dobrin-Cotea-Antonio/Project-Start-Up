using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourWithPause {

    [Header("Prefab Data")]
    [SerializeField] GameObject[] abilityCards;

    [Header("LimbPrefabs")]
    [SerializeField] GameObject[] limbs;

    [Header("HpPrefab")]
    [SerializeField] GameObject hp;

    [Header("DoorIcons")]
    [SerializeField] Sprite[] _sprites; 

    public GameObject[] _abilityCards{ get { return abilityCards; } private set { abilityCards = value; } }
    public Sprite[] sprites { get { return _sprites; } private set { _sprites = value; } }

    public static RoomManager roomManager { get; private set; }

    DoorScript[] doors;
    EnemyManager enemyManager;
    GameObject dropPoint;
    GameManager gameManager;

    private void Awake(){
        roomManager = this;
        enemyManager = GetComponent<EnemyManager>();
        enemyManager.OnRoomClear += OnRoomClear;
        doors = FindObjectsOfType<DoorScript>();
        dropPoint = GameObject.FindGameObjectWithTag("DropPoint");


    }

    private void Start(){
        gameManager = GameManager.gameManager;
    }

    void OnRoomClear() {

        SpawnRoomReward();

        foreach (DoorScript d in doors)
            d.ActivateDoor(GameManager.gameManager.nextRoomRewards[d.doorID]);

        enemyManager.OnRoomClear -= OnRoomClear;
    }

    void SpawnRoomReward() {
        dropPoint.transform.position = gameManager.player.transform.position;
        ArrowManager.arrowManager.ChangeStatus(true);

        switch (gameManager.currentRoomReward) {
            case GameManager.RewardTypes.Ability:
                SpawnAbility();
                break;
            case GameManager.RewardTypes.Hp:
                SpawnHp();
                break;
            case GameManager.RewardTypes.Limb:
                SpawnLimbs();
                break;
        }
            
    
    
    }

    void SpawnAbility(bool pIsShopItem=false) {

        List<int> abilityIndexes = new List<int>();
        for (int i = 0; i < 4; i++)
            if (i != gameManager.abilityManager._activeAbilityIndex1 && i != gameManager.abilityManager._activeAbilityIndex2)
                abilityIndexes.Add(i);


        int abilityIndex = Random.Range(0, abilityIndexes.Count);

        GameObject g = Instantiate(abilityCards[abilityIndexes[abilityIndex]], dropPoint.transform.position, Quaternion.identity);

        if (pIsShopItem){
            InteractableData data = g.GetComponent<InteractableData>();
            data.isShopItem = true;
            data.levelCost = 200;
        }
    }

    void SpawnHp(bool pIsShopItem = false) {

        GameObject g = Instantiate(hp, dropPoint.transform.position, Quaternion.identity);
        if (pIsShopItem) {
            InteractableData data = g.GetComponent<InteractableData>();
            data.isShopItem = true;
            data.levelCost = 100;
        }
    
    }

    void SpawnLimbs(bool pIsShopItem = false) {
        int limbIndex = Random.Range(0, limbs.Length);
        GameObject g = Instantiate(limbs[limbIndex],dropPoint.transform.position,Quaternion.identity);

        if (pIsShopItem){
            InteractableData data = g.GetComponent<InteractableData>();
            data.isShopItem = true;
            data.levelCost = 250;
        }

    }
}
