using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviourWithPause {

    [SerializeField] GameObject arrow1;
    [SerializeField] GameObject arrow2;

    DoorScript[] doors;

    public static ArrowManager arrowManager;

    private void Awake() {
        arrowManager = this;
        enabled = false;
    }

    private void Start() {
        doors = FindObjectsOfType<DoorScript>();

    }

    protected override void UpdateWithPause() {

        Vector3 dir = (doors[0].transform.position - arrow1.transform.position);

        arrow1.transform.rotation = Quaternion.LookRotation(dir.normalized);

        dir = (doors[1].transform.position - arrow1.transform.position);

        arrow2.transform.rotation = Quaternion.LookRotation(dir.normalized);

    }

    public void ChangeStatus(bool pState) {
        enabled = pState;
    }

}
