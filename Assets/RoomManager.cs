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

        gameManager.currentRoomReward = GameManager.RewardTypes.Ability;//DELETE LATER


        switch (gameManager.currentRoomReward) {
            case GameManager.RewardTypes.Ability:
                SpawnAbility();
                break;

        }
            
    
    
    }

    void SpawnAbility() {
        Debug.Log(abilityCards.Length);

        Debug.Log(_abilityCards[0]);

        int abilityIndex = Random.Range(0, abilityCards.Length);
        Instantiate(abilityCards[abilityIndex], dropPoint.transform.position, Quaternion.identity);
    }
}
