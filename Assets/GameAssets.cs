using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    public Sprite HealthRegen;
    public Sprite Limbs_1;
    public Sprite Limbs_2;
    public Sprite Limbs_3;
    public Sprite Limbs_4;
    public Sprite Ability_1;
    public Sprite Ability_2;
    public Sprite Ability_3;
    public Sprite Ability_4;
    public Sprite Grenade;
    
}
