using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviourWithPause {

    [Header("Data")]
    public Transform[] shootPositions = new Transform[2];
    [SerializeField] Transform refPoint;
    [SerializeField] GameObject modelHolder;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LayerMask maskGround;
    [SerializeField] LayerMask maskTargets;
    [SerializeField] Animator animator;

    [Header("Shooting Data")]
    [SerializeField] float spreadHipFire;//spread in degrees (final spread angle/2)
    [SerializeField] float spreadAim;
    [SerializeField] float shotCooldown;

    [SerializeField] float aimSpeedDecrease;//in percentage

    int weaponToFire = 0;

    Vector3 aimPosition;
    float lastShotTime=-1000000;

    PlayerControls player;
    InputManager input;
    PlayerStatsData data;

    void Start() {
        player = GetComponent<PlayerControls>();
        input = GetComponent<InputManager>();
        data = GetComponent<PlayerStatsData>();
        animator.SetBool("shootLeft", true);
    }

    protected override void UpdateWithPause() {

        ChooseState();

        if (player.attackState == PlayerControls.AttackStates.Idle) {
            return;
        }

        CastRay();

        StateMachine();
    }


    void CastRay() {

        Ray cameraRay;
        RaycastHit cameraRayHit;

        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out cameraRayHit, Mathf.Infinity, maskTargets)){

            aimPosition = new Vector3(cameraRayHit.point.x, shootPositions[weaponToFire].transform.position.y, cameraRayHit.point.z);
            Vector3 direction = cameraRayHit.point - modelHolder.transform.position;
            direction.y = 0;
            direction.Normalize();

            float angle = Vector3.SignedAngle(refPoint.forward,(cameraRayHit.point - refPoint.position),Vector3.up);

            Debug.Log(angle);
            animator.SetFloat("angle",angle);


            modelHolder.transform.forward = direction;
            refPoint.transform.forward = direction;
            



            return;

        }

        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(cameraRay, out cameraRayHit, Mathf.Infinity, maskGround)){

            //Debug.DrawLine(cameraRayHit.point+new Vector3(0,10,0),cameraRayHit.point,Color.red,0.1f);
            
            aimPosition = new Vector3(cameraRayHit.point.x, cameraRayHit.point.y, cameraRayHit.point.z);
            Vector3 direction = cameraRayHit.point - modelHolder.transform.position;
            direction.y = 0;
            direction.Normalize();

            modelHolder.transform.forward = direction;
            refPoint.transform.forward = direction;
            

        }

    }


    void ChooseState() {
        if (!input.shootInputHold&&!input.aimInput){
            if (player.attackState == PlayerControls.AttackStates.Idle)
                return;

            data.AddMovementModifier("AimBonus", 0);
            if (player.movementState == PlayerControls.MovementStates.Walk || player.movementState == PlayerControls.MovementStates.Dash){
                animator.SetInteger("gunState",1);
            }
            else {
                animator.SetInteger("gunState", 0);
            }


            player.attackState = PlayerControls.AttackStates.Idle;
            return;
        }


        if (input.shootInputHold&&!input.aimInput){
            if (player.attackState == PlayerControls.AttackStates.HipFire)
                return;

            data.AddMovementModifier("AimBonus", 0);
            animator.SetInteger("gunState", 2);

            player.attackState = PlayerControls.AttackStates.HipFire;

            return;
        }


        if (!input.shootInputHold && input.aimInput) {
            if (player.attackState == PlayerControls.AttackStates.Aim)
                return;

            data.AddMovementModifier("AimBonus", aimSpeedDecrease);
            animator.SetInteger("gunState", 2);

            player.attackState = PlayerControls.AttackStates.Aim;
            return;
        }


        if (input.aimInput && input.shootInputHold) {
            if (player.attackState == PlayerControls.AttackStates.AimAndShoot)
                return;

            data.AddMovementModifier("AimBonus", aimSpeedDecrease);
            animator.SetInteger("gunState", 3);

            player.attackState = PlayerControls.AttackStates.AimAndShoot;
            return;
        }

    }

    void StateMachine() {
        switch (player.attackState) {
            case PlayerControls.AttackStates.Idle:
                
                break;
            case PlayerControls.AttackStates.HipFire:
                //ShootBullet(spreadHipFire);
                break;

            case PlayerControls.AttackStates.Aim:
                
                break;

            case PlayerControls.AttackStates.AimAndShoot:
                
                //ShootBullet(spreadAim);
                break;

        }

    }

    public void ShootBullet() {
        //if (Time.time - lastShotTime >= shotCooldown*data.shootSpeedMultiplier){

        if (Time.time - lastShotTime < 0.1f)
            return;

            float pSpread = 0f;

            if (player.attackState == PlayerControls.AttackStates.AimAndShoot) 
                pSpread = spreadAim;


            if (player.attackState == PlayerControls.AttackStates.HipFire)
                pSpread = spreadHipFire;

            lastShotTime = Time.time;
            GameObject b1 = Instantiate(bulletPrefab, shootPositions[weaponToFire].position, Quaternion.identity);
            
            Bullet bullet1 = b1.GetComponent<Bullet>();
            bullet1.speed = 60;
            bullet1.range = 50;
            bullet1.damage = 10;

            float randomAngle = Random.Range(-pSpread, pSpread);
            bullet1.transform.forward = Quaternion.Euler(0, 90+randomAngle, 0) * shootPositions[weaponToFire].forward;
            bullet1.AddSpeed(Quaternion.Euler(0,randomAngle,0)* shootPositions[weaponToFire].forward);
            weaponToFire = (weaponToFire + 1) % 2;

            if (weaponToFire == 0){
                Debug.Log("true");
                animator.SetBool("shootLeft", true);
            }

            else {
                Debug.Log("false");
                animator.SetBool("shootLeft", false);
            }
                

        //}
    }
}
