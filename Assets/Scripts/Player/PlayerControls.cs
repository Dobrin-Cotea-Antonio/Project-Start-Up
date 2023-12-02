using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControls : MonoBehaviourWithPause {
    // Start is called before the first frame update

    Rigidbody rb;
    InputManager input;
    [Header("Data")]
    [SerializeField] Transform refPoint;
    [SerializeField] Camera cameraMain;
    [SerializeField] GameObject interactionSphere;
    [SerializeField] GameObject modelHolder;
    [SerializeField] Animator animator;


    [Header("Basic Interaction")]
    [SerializeField] float moveSpeed;
    [SerializeField] float interactionRange;

    [Header("Dash")]
    [SerializeField] float dashRange;
    [SerializeField] float dashDuration;//in seconds
    [SerializeField] float dashCooldown;//in seconds
    [SerializeField] int dashChargesMax;

    int availableDashCharges;


    // public float movementSpeedMultiplier { get; set; }
    //public float baseMovementSpeedMultiplier { get; private set; }

    PlayerStatsData data;


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

       right = Vector3.zero;
        forward = Vector3.zero;

        movementState = MovementStates.Walk;
        attackState = AttackStates.Idle;

        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputManager>();
        data = GetComponent<PlayerStatsData>();

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
                break;
            case MovementStates.Walk:
                Walk();
                break;
            case MovementStates.Dash:
                break;

        }

    }

    void StateMachineChooseState() {
        ChooseWalkAnimationState();

        switch (movementState){
            case MovementStates.Idle:   
                IdleState();
                break;
            case MovementStates.Walk:
                WalkState();
                break;
            case MovementStates.Dash:
                break;

        }
    }

    protected override void FixedUpdateWithPause() {
        StateMachineExecution();
    }

    void WalkState() {
        Vector3 direction = right * input.moveDirection.x + forward * input.moveDirection.z;

        Vector3 force = direction * moveSpeed * data.movementSpeedMultiplier;

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
        Vector3 force = walkDirection * moveSpeed * data.movementSpeedMultiplier;

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
        float speed = dashRange * (1 / dashDuration)*data.dashSpeedMultiplier;

        rb.AddForce(speed*pDirection, ForceMode.VelocityChange);

        yield return new WaitForSecondsRealtime(dashDuration);

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
        yield return new WaitForSecondsRealtime(dashCooldown);
        availableDashCharges = Mathf.Min(availableDashCharges + 1,dashChargesMax);
    }

    void SetModelDirection(Vector3 pDirection) {
        DOTween.defaultTimeScaleIndependent = true;

        if (attackState != AttackStates.Idle)
            return;

        Vector3 newForward = new Vector3(pDirection.x, 0, pDirection.z).normalized;

        if (newForward == modelHolder.transform.forward)
            return;

        float t = 0.15f;

        if (newForward == -modelHolder.transform.forward)
            StartCoroutine(TurnAround(t, newForward));
        else{
            DOTween.To(() => modelHolder.transform.forward, x => modelHolder.transform.forward = x, newForward, t);
            DOTween.To(() => refPoint.forward, x => refPoint.forward = x, newForward, t);
        }

    }

    void ChooseWalkAnimationState() {
        if (attackState == AttackStates.Idle) {
            if (rb.velocity.magnitude <= 0.01){
                animator.SetFloat("X", 0f);
                animator.SetFloat("Y", 0f);
            }
            else {
                animator.SetFloat("X", 0f);
                animator.SetFloat("Y", -1f);
            }

            return;
        }

        if (rb.velocity.magnitude <= 0.01){
            animator.SetFloat("X", 0f);
            animator.SetFloat("Y", 0f);
            return;
        }

        Vector3 vector = modelHolder.transform.InverseTransformDirection(walkDirection * moveSpeed * data.movementSpeedMultiplier);

        Vector2 v = new Vector2(vector.x, vector.z);
        v.Normalize();

        animator.SetFloat("X", v.x);
        animator.SetFloat("Y", v.y);

        //Debug.Log(v);
        //tweeen to be smooth   

    }

    IEnumerator TurnAround(float pTime,Vector3 pForward) {

        Vector3 endPoint = -pForward;
        Vector3 midPoint = Vector3.Cross(Vector3.up, pForward);

        DOTween.To(() => modelHolder.transform.forward, x => modelHolder.transform.forward = x, midPoint, pTime/2);

        yield return new WaitForSecondsRealtime(pTime/2);

        DOTween.To(() => modelHolder.transform.forward, x => modelHolder.transform.forward = x, endPoint, pTime / 2);

    }

}
