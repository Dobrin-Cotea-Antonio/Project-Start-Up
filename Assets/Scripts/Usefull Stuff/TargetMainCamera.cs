using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMainCamera : MonoBehaviourWithPause{

    protected override void UpdateWithPause(){

        transform.LookAt(transform.position +Camera.main.transform.rotation*Vector3.forward,Camera.main.transform.rotation*Vector3.up);
    }
}
