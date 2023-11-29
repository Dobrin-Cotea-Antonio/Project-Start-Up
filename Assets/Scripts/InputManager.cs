using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviourWithPause
{
    //[SerializeField] Transform rotationPivot;
    Controls controls;  
    public Vector3 moveDirection { get; private set; }
    public bool jumpInput { get; private set; }//remove
    public bool shootInputHold { get; private set; }
    public bool shootInputClick { get; private set; }
    public bool reloadInput { get; private set; }
    public bool aimInput { get; private set; }
    public bool shopInput { get; private set; }
    public bool interactionInput { get; private set; }
    public bool interactionInput2 { get; private set; }
    public bool ability1Input { get; private set; }
    public bool ability2Input { get; private set; }
    public bool spawnBot { get; private set; }
    public bool sprintInput { get; private set; }
    public bool dashInput { get; private set; }//new 

    private void Awake()
    {
        ignorePausedState = true;
    }

    private void Start()
    {

        controls = Controls.controls;

    }

    protected override void UpdateWithPause()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        moveDirection.Normalize();
        //Debug.Log(moveDirection);
        //moveDirection = Quaternion.Euler(new Vector3(0, rotationPivot.transform.localEulerAngles.y, 0)) * moveDirection;
        jumpInput = Input.GetKey(controls.keyList["jump"]);
        shootInputHold = Input.GetKey(controls.keyList["shoot"]);
        shootInputClick = Input.GetKeyDown(controls.keyList["shoot"]);
        reloadInput = Input.GetKeyDown(controls.keyList["reload"]);
        aimInput = Input.GetKey(controls.keyList["aim"]);
        shopInput = Input.GetKeyUp(controls.keyList["shop"]);
        interactionInput = Input.GetKeyDown(controls.keyList["interact"]);
        interactionInput2 = Input.GetKeyDown(controls.keyList["interact2"]);
        ability1Input = Input.GetKeyDown(controls.keyList["ability1"]);
        ability2Input = Input.GetKeyDown(controls.keyList["ability2"]);
        spawnBot = Input.GetKeyDown(controls.keyList["robotSpawn"]);
        sprintInput = Input.GetKey(controls.keyList["sprint"]);

        dashInput = Input.GetKeyDown(controls.keyList["dash"]);

    }

}
