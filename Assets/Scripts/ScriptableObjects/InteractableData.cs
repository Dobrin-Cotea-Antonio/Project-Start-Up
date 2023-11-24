using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "BaseInteractableData")]
public class InteractableData : MonoBehaviourWithPause{

    public Action OnPickUp;

    [Header("")]
    public GameObject UI;

    public void PickUp() {
        OnPickUp?.Invoke();
    }



}
