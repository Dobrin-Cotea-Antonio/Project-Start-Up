using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        HealthRegen,
        Limbs_1,
        Limbs_2,
        Limbs_3,
        Limbs_4,
        Ability_1,
        Ability_2,
        Ability_3,
        Grenade
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthRegen: return 50;
            case ItemType.Limbs_1: return 200;
            case ItemType.Limbs_2: return 500;
            case ItemType.Limbs_3: return 1000;
            case ItemType.Limbs_4: return 1500;
            case ItemType.Ability_1: return 500;
            case ItemType.Ability_2: return 500;
            case ItemType.Ability_3: return 500;
            case ItemType.Grenade: return 100;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Limbs_1: return GameAssets.i.Limbs_1;
            case ItemType.Limbs_2: return GameAssets.i.Limbs_2;
        }
    }
}
