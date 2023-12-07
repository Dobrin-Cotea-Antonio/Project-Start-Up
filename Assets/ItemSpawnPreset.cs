using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPreset : MonoBehaviourWithPause{

    [SerializeField] GameManager.RewardTypes type;
    [SerializeField] bool isShopItem;

    bool spawnedItem = false;


    protected override void UpdateWithPause() {
        if (!spawnedItem) {
            SpawnItem();
            spawnedItem = true;
        }
    }

    void SpawnItem() {

        switch (type){
            case GameManager.RewardTypes.Ability:
                if (isShopItem)
                    RoomManager.roomManager.SpawnAbility(true, transform.position.x, transform.position.y, transform.position.z);
                else 
                    RoomManager.roomManager.SpawnAbility(false, transform.position.x, transform.position.y, transform.position.z);
                break;
            case GameManager.RewardTypes.Hp:
                if (isShopItem)
                    RoomManager.roomManager.SpawnHp(true, transform.position.x, transform.position.y, transform.position.z);
                else
                    RoomManager.roomManager.SpawnHp(false, transform.position.x, transform.position.y, transform.position.z);
                break;
            case GameManager.RewardTypes.Limb:
                if (isShopItem)
                    RoomManager.roomManager.SpawnLimbs(true, transform.position.x, transform.position.y, transform.position.z);
                else
                    RoomManager.roomManager.SpawnLimbs(false, transform.position.x, transform.position.y, transform.position.z);
                break;


        }

    }
}
