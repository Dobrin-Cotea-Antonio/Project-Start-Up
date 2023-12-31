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
    [SerializeField] GameObject muzzleFlashPrefab;
    [SerializeField] float spreadHipFire;//spread in degrees (final spread angle/2)
    [SerializeField] float spreadAim;
    [SerializeField] float shotCooldown;

    [SerializeField] float aimSpeedDecrease;//in percentage
    [SerializeField] float damageBullet;
    [SerializeField] float rangeBullet;
    [SerializeField] float speedBullet;

    int weaponToFire = 0;

    Vector3 aimPosition;
    float lastShotTime=-1000000;
    Vector3 bulletAimPos;

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

        bulletAimPos = Vector3.zero;

        if (Physics.Raycast(cameraRay, out cameraRayHit, Mathf.Infinity, maskTargets)){

            bulletAimPos = cameraRayHit.point;

            //Debug.Log(cameraRayHit.collider.gameObject);

            aimPosition = new Vector3(cameraRayHit.point.x, shootPositions[weaponToFire].transform.position.y, cameraRayHit.point.z);
            Vector3 direction = cameraRayHit.point - modelHolder.transform.position;
            direction.y = 0;
            direction.Normalize();

            float angle = Vector3.SignedAngle(refPoint.forward,(cameraRayHit.point - refPoint.position),Vector3.up);

            animator.SetFloat("angle",angle);

            modelHolder.transform.forward = direction;
            refPoint.transform.forward = direction;

            return;

        }

        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, refPoint.position);
        float rayLength;
        
        if (groundPlane.Raycast(cameraRay, out rayLength)){

            //Physics.Raycast(cameraRay, out cameraRayHit, Mathf.Infinity, maskGround)

            //Debug.DrawLine(cameraRayHit.point+new Vector3(0,10,0),cameraRayHit.point,Color.red,0.1f);

            //aimPosition = new Vector3(cameraRayHit.point.x, cameraRayHit.point.y, cameraRayHit.point.z);
            //Vector3 direction = cameraRayHit.point - modelHolder.transform.position;
            ////direction.y = 0;
            //direction.Normalize();

            //modelHolder.transform.forward = direction;
            //refPoint.transform.forward = direction;

            aimPosition = cameraRay.GetPoint(rayLength);

            Debug.DrawLine(cameraRay.origin, aimPosition);

            Vector3 direction = Quaternion.Euler(0,-4,0)*(aimPosition - modelHolder.transform.position);
            direction.y = 0;
            direction.Normalize();

            modelHolder.transform.forward = direction;
            refPoint.transform.forward = direction;
        }

    }

    void ChooseState() {
        animator.SetFloat("shootSpeedMultiplier", data.shootSpeedMultiplier);

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
            //animator.SetBool("hipFire", true);
            animator.SetBool("isAiming", false);

            player.attackState = PlayerControls.AttackStates.HipFire;

            return;
        }


        if (!input.shootInputHold && input.aimInput) {
            if (player.attackState == PlayerControls.AttackStates.Aim)
                return;

            data.AddMovementModifier("AimBonus", aimSpeedDecrease);
            animator.SetInteger("gunState", 2);
            animator.SetBool("isAiming", true);

            player.attackState = PlayerControls.AttackStates.Aim;
            return;
        }


        if (input.aimInput && input.shootInputHold) {
            if (player.attackState == PlayerControls.AttackStates.AimAndShoot)
                return;

            data.AddMovementModifier("AimBonus", aimSpeedDecrease);
            animator.SetInteger("gunState", 3);
            animator.SetBool("isAiming", true);

            player.attackState = PlayerControls.AttackStates.AimAndShoot;
            return;
        }

    }

    void StateMachine() {
        switch (player.attackState) {
            case PlayerControls.AttackStates.Idle:
                
                break;
            case PlayerControls.AttackStates.HipFire:
                break;

            case PlayerControls.AttackStates.Aim:
                
                break;

            case PlayerControls.AttackStates.AimAndShoot:
                
                break;

        }

    }

    public void ShootBullet() {

        float pSpread = 0f;

        if (player.attackState == PlayerControls.AttackStates.AimAndShoot)
            pSpread = spreadAim;


        if (player.attackState == PlayerControls.AttackStates.HipFire)
            pSpread = spreadHipFire;

        lastShotTime = Time.time;
        GameObject b1 = Instantiate(bulletPrefab, shootPositions[weaponToFire].position, Quaternion.identity);
        Instantiate(muzzleFlashPrefab, shootPositions[weaponToFire].position, refPoint.rotation, shootPositions[weaponToFire]);

        Bullet bullet1 = b1.GetComponent<Bullet>();
        bullet1.speed = speedBullet;
        bullet1.range = rangeBullet;
        bullet1.damage = damageBullet;

        float randomAngle = Random.Range(-pSpread, pSpread);
        bullet1.transform.forward = Quaternion.Euler(0, 90 + randomAngle, 0) * shootPositions[weaponToFire].forward;

        Vector3 dir = Vector3.zero;
        if (bulletAimPos != Vector3.zero){
            dir = bulletAimPos - shootPositions[weaponToFire].position;
            dir.Normalize();
        }
        else{
            dir = shootPositions[weaponToFire].forward;
            dir.Normalize();
        }


        bullet1.AddSpeed(dir*data.bulletSpeedMultiplier);

        weaponToFire = (weaponToFire + 1) % 2;

        if (weaponToFire == 0)
            animator.SetBool("shootLeft", true);
        else
            animator.SetBool("shootLeft", false);


    }
}
