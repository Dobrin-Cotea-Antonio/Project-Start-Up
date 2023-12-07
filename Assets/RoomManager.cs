using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourWithPause {

    [Header("Prefab Data")]
    [SerializeField] GameObject[] abilityCards;

    public GameObject[] _abilityCards{ get { return abilityCards; } private set { abilityCards = value; } }

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
            d.gameObject.SetActive(true);

        enemyManager.OnRoomClear -= OnRoomClear;
    }

    void SpawnRoomReward() {
        dropPoint.transform.position = gameManager.player.transform.position;
        ArrowManager.arrowManager.ChangeStatus(true);
        Debug.Log(ArrowManager.arrowManager);

        Debug.Log(gameManager.currentRoomReward);

        switch (gameManager.currentRoomReward) {
            case GameManager.RewardTypes.Ability:
                SpawnAbility();
                break;

        }
            
    
    
    }

    void SpawnAbility() {

        List<int> abilityIndexes = new List<int>();
        for (int i = 0; i < 4; i++)
            if (i != gameManager.abilityManager._activeAbilityIndex1 && i != gameManager.abilityManager._activeAbilityIndex2)
                abilityIndexes.Add(i);


        int abilityIndex = Random.Range(0, abilityIndexes.Count);

        Instantiate(abilityCards[abilityIndexes[abilityIndex]], dropPoint.transform.position, Quaternion.identity);
    }
}
