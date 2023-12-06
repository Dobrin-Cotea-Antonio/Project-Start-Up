using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEvent : MonoBehaviourWithPause{
    [SerializeField] SuicideBomberScript bomber;

    public void Explode() {
        bomber.SpawnExplosion();
    }
}
