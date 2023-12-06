using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAnimation : MonoBehaviourWithPause{

    [SerializeField] float yDif;//in meters/unity units
    [SerializeField] float speed;//in degrees per second

    Vector3 startPos;

    //Update is called once per frame

    private void Start(){
        startPos = transform.position;
    }
    protected override void UpdateWithPause(){
        transform.rotation = Quaternion.Euler(0, speed*Time.deltaTime, 0) * transform.rotation;
        transform.position = startPos + new Vector3(0, yDif, 0)*  Mathf.Sin(transform.rotation.eulerAngles.y*Mathf.PI/180);
    }
}
