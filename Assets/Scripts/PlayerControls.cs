using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControls : MonoBehaviourWithPause {
    // Start is called before the first frame update

    Rigidbody rb;
    InputManager input;
    [Header("Data")]
    [SerializeField] Camera cameraMain;
    [SerializeField] GameObject interactionSphere;
    [SerializeField] GameObject modelHolder;


    [Header("Basic Interaction")]
    [SerializeField] float moveSpeed;
    [SerializeField] float interactionRange;

    [Header("Dash")]
    [SerializeField] float dashRange;
    [SerializeField] float dashDuration;//in seconds
    [SerializeField] float dashCooldown;//in seconds
    [SerializeField] int dashChargesMax;

    int availableDashCharges;

    [Header("Stats")]
    [SerializeField] float maxHp;
    

    public float movementSpeedMultiplier { get; set; }
    public float baseMovementSpeedMultiplier { get; private set; }




    public Vector3 right { get; private set; }
    public Vector3 forward { get; private set; }

    Vector3 walkDirection = Vector3.zero;

    //fix state machine bcz its shit
    public enum MovementStates {
        Idle,
        Walk,
        Dash
    }

    public enum AttackStates {
        Idle,
        HipFire,//hip fire; will have more spread but keep normal running speed
        Aim,//decreased run speed but almost perfect accuracy
        AimAndShoot
    }

    public MovementStates movementState { get; set; }
    public AttackStates attackState { get; set; }

    private void Awake(){
        
    }

    void Start() {
        GameManager.gameManager.player = gameObject;

        baseMovementSpeedMultiplier = 1;
        movementSpeedMultiplier = baseMovementSpeedMultiplier;
        

        right = Vector3.zero;
        forward = Vector3.zero;

        movementState = MovementStates.Walk;
        attackState = AttackStates.Idle;

        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputManager>();

        interactionSphere.transform.localScale = new Vector3(interactionRange, interactionRange, interactionRange);
        availableDashCharges = dashChargesMax;
    }

    protected override void UpdateWithPause() {
        if (right == Vector3.zero) {

            Quaternion cameraRotation = cameraMain.transform.rotation;
            cameraMain.transform.rotation = Quaternion.Euler(0, cameraMain.transform.eulerAngles.y, 0);
            right = cameraMain.transform.right;
            forward = cameraMain.transform.forward;
            cameraMain.transform.rotation = cameraRotation;
        }


        StateMachineChooseState();

    }

    void StateMachineExecution(){
        switch (movementState){
            case MovementStates.Idle:
                //IdleState();
                break;
            case MovementStates.Walk:
                Walk();
                break;
            case MovementStates.Dash:
                //DashState();
                break;

        }

    }

    void StateMachineChooseState() {
        switch (movementState){
            case MovementStates.Idle:
                IdleState();
                break;
            case MovementStates.Walk:
                WalkState();
                break;
            case MovementStates.Dash:
                //DashState();
                break;

        }
    }

    protected override void FixedUpdateWithPause() {
        StateMachineExecution();
    }

    void WalkState() {
        Vector3 direction = right * input.moveDirection.x + forward * input.moveDirection.z;

        Vector3 force = direction * moveSpeed * movementSpeedMultiplier;

        if (force.magnitude < 0.001) {
            movementState = MovementStates.Idle;
            return;
        }

        walkDirection = direction;

        if (StartDash(direction))
            return;

    }

    void Walk(){

        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        Vector3 force = walkDirection * moveSpeed * movementSpeedMultiplier;

        SetModelDirection(force);
        rb.AddForce(force, ForceMode.VelocityChange);

    }

    void IdleState() {
        if (input.moveDirection.magnitude != 0){
            movementState = MovementStates.Walk;
            return;
        }

        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        StartDash(right);

    }



    IEnumerator Dash(Vector3 pDirection) {
        float speed = dashRange * (1 / dashDuration);

        rb.AddForce(speed*pDirection, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        StopDash();
    }

    bool StartDash(Vector3 pDirection) {
        if (input.dashInput && availableDashCharges > 0) {
            SetModelDirection(pDirection);
            availableDashCharges = Mathf.Max(0, availableDashCharges - 1);
            movementState = MovementStates.Dash;
            StartCoroutine(Dash(pDirection));
            StartCoroutine(RecoverDashCharge());
            return true;
        }
        return false;
    }

    void StopDash() {
        movementState = MovementStates.Idle;
        rb.velocity = new Vector3(0,rb.velocity.y,0);
    }

    IEnumerator RecoverDashCharge() {
        yield return new WaitForSeconds(dashCooldown);
        availableDashCharges = Mathf.Min(availableDashCharges + 1,dashChargesMax);
    }

    void SetModelDirection(Vector3 pDirection) {//instead of setting the rotation instantly rotate it by a certain amount until the desired rotation is reached
        if (attackState == AttackStates.Idle) {
            //Debug.Log(attackState);
            modelHolder.transform.forward = new Vector3(pDirection.x, 0, pDirection.z).normalized;
        }

    }
}
