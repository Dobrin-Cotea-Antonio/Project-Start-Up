using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemyAnimTest : MonoBehaviour
{

    [SerializeField] private Animator animator;

    private bool isRolling = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isRolling = !isRolling;
            if (isRolling)
            {
                animator.SetBool("Rolling", false);
            }
            else
            {
                animator.SetBool("Rolling", true);
            }
        }
    }
}
