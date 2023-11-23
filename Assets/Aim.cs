using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviourWithPause {

    [Header("Data")]
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject modelHolder;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LayerMask maskGround;
    [SerializeField] LayerMask maskTargets;
    [SerializeField] GameObject spreadIndicator;
    [SerializeField] Transform spreadLeft;
    [SerializeField] Transform spreadRight;

    [Header("Shooting Data")]
    [SerializeField] float spreadHipFire;//spread in degrees (final spread angle/2)
    [SerializeField] float spreadAim;
    [SerializeField] float shotCooldown;

    [SerializeField] float aimSpeedDecrease;//in percentage


    Vector3 aimPosition;
    float lastShotTime=-1000000;

    PlayerControls player;
    InputManager input;

    void Start() {
        player = GetComponent<PlayerControls>();
        input = GetComponent<InputManager>();

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

            aimPosition = new Vector3(cameraRayHit.point.x, shootPosition.transform.position.y, cameraRayHit.point.z);
            Vector3 direction = cameraRayHit.point - modelHolder.transform.position;
            //direction.y = 0;
            direction.Normalize();
            modelHolder.transform.forward = direction;
            return;

        }

        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out cameraRayHit, Mathf.Infinity, maskGround)){

            aimPosition = new Vector3(cameraRayHit.point.x, shootPosition.transform.position.y, cameraRayHit.point.z);
            Vector3 direction = cameraRayHit.point - modelHolder.transform.position;
            direction.y = 0;
            direction.Normalize();
            modelHolder.transform.forward = direction;

        }

    }


    void ChooseState() {
        if (!input.shootInputHold&&!input.aimInput){
            if (player.attackState == PlayerControls.AttackStates.Idle)
                return;

            spreadIndicator.SetActive(false);
            player.movementSpeedMultiplier = player.baseMovementSpeedMultiplier;
            player.attackState = PlayerControls.AttackStates.Idle;
            return;
        }


        if (input.shootInputHold&&!input.aimInput){
            if (player.attackState == PlayerControls.AttackStates.HipFire)
                return;

            spreadIndicator.SetActive(true);
            spreadLeft.localEulerAngles = new Vector3(0, -spreadHipFire, 0);
            spreadRight.localEulerAngles = new Vector3(0, spreadHipFire, 0);
            player.movementSpeedMultiplier = player.baseMovementSpeedMultiplier;
            player.attackState = PlayerControls.AttackStates.HipFire;

            return;
        }


        if (!input.shootInputHold && input.aimInput) {
            if (player.attackState == PlayerControls.AttackStates.Aim)
                return;

            spreadIndicator.SetActive(true);
            spreadLeft.localEulerAngles = new Vector3(0, -spreadAim, 0);
            spreadRight.localEulerAngles = new Vector3(0, spreadAim, 0);
            player.movementSpeedMultiplier = ((100f-aimSpeedDecrease)/100)*player.baseMovementSpeedMultiplier;
            player.attackState = PlayerControls.AttackStates.Aim;
            return;
        }


        if (input.aimInput && input.shootInputHold) {
            if (player.attackState == PlayerControls.AttackStates.AimAndShoot)
                return;

            spreadIndicator.SetActive(true);
            spreadLeft.localEulerAngles = new Vector3(0, -spreadAim, 0);
            spreadRight.localEulerAngles = new Vector3(0, spreadAim, 0);
            player.movementSpeedMultiplier = ((100f - aimSpeedDecrease) / 100) * player.baseMovementSpeedMultiplier;
            player.attackState = PlayerControls.AttackStates.AimAndShoot;
            return;
        }

    }

    void StateMachine() {
        switch (player.attackState) {
            case PlayerControls.AttackStates.Idle:

                break;
            case PlayerControls.AttackStates.HipFire:
                if (Time.time - lastShotTime >= shotCooldown) {

                    //shoot mode 1 works mostly well but when aiming at yourself you shoot youself

                    //lastShotTime = Time.time;
                    //GameObject b = Instantiate(bulletPrefab, shootPosition.position,Quaternion.identity);
                    //Bullet bullet = b.GetComponent<Bullet>();
                    //bullet.speed = 30;
                    //bullet.range = 50;
                    //bullet.damage = 10;
                    //Vector3 dir = (aimPosition - shootPosition.position).normalized;

                    //bullet.transform.forward = Quaternion.Euler(0, 90, 0) * dir;

                    //bullet.AddSpeed(dir);


                    //shoot mode 2

                    //lastShotTime = Time.time;
                    //GameObject b = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
                    //Bullet bullet = b.GetComponent<Bullet>();
                    //bullet.speed = 60;
                    //bullet.range = 50;
                    //bullet.damage = 10;
                    //bullet.transform.forward = Quaternion.Euler(0, 90, 0) * shootPosition.forward;
                    //bullet.AddSpeed(shootPosition.forward);

                    ShootBullet(spreadHipFire);



                }
                break;
            case PlayerControls.AttackStates.Aim:
                
                break;

            case PlayerControls.AttackStates.AimAndShoot:
                ShootBullet(spreadAim);
                break;


        }



    }

    void ShootBullet(float pSpread) {
        if (Time.time - lastShotTime >= shotCooldown){
            lastShotTime = Time.time;
            GameObject b1 = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
            Bullet bullet1 = b1.GetComponent<Bullet>();
            bullet1.speed = 60;
            bullet1.range = 50;
            bullet1.damage = 10;

            float randomAngle = Random.Range(-pSpread, pSpread);
            bullet1.transform.forward = Quaternion.Euler(0, 90+randomAngle, 0) * shootPosition.forward;
            bullet1.AddSpeed(Quaternion.Euler(0,randomAngle,0)*shootPosition.forward);
        }
    }
}
