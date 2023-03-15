using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float maxSpeed = 5.0f;
    //Ground and Jump control
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float attackRate = 0.8f;

    //Controllers and references
    Rigidbody2D rbREF;
    private float movementSpeed;
    private bool bIsfacingRight = true;
    private bool bIsOnGround = true;
    private bool bJump = false;
    private bool bDoubleJump;
    private Animator amim;
    private Attack attack;

    private float attackCounter = 0.0f;


    //Items
    private Weapons weaponInfo;



    // Start is called before the first frame update
    void Start()
    {
        rbREF= GetComponent<Rigidbody2D>();
        amim= GetComponent<Animator>(); 
        movementSpeed = maxSpeed;
        attack= GetComponentInChildren<Attack>();
    }

    // Update is called once per frame
    void Update()
    {

        Attack();
 
        JumpAndDoubleJumpCheck();
       
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        Jump();
    }


    #region Movement animation and flip
    private void PlayerMovement()
    {

        //Move right and left
        float h = Input.GetAxisRaw("Horizontal");
        float v = rbREF.velocity.y;

        rbREF.velocity = new Vector2(h * movementSpeed, v);

        //when to flip the prite based on the direction

        if (h < 0 && bIsfacingRight)
        {
            Flip();
        }else if(h > 0 && !bIsfacingRight)
        {
            Flip();
        }
    }

    //Flip player sprite
    private void Flip()
    {
        bIsfacingRight = !bIsfacingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    private void GroundCheck()
    {
        //checking if on the ground
        bIsOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, layerGround);
    }


    private void JumpAndDoubleJumpCheck()
    {

        GroundCheck();

        if (bIsOnGround)
        {
            bDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && (bIsOnGround || !bDoubleJump))
        {
            bJump = true;

            if(!bDoubleJump && !bIsOnGround)
            {
                bDoubleJump = true;
            }
        }
    }


    private void Jump()
    {

        if (bJump)
        {
            rbREF.velocity = Vector2.zero;
            rbREF.velocity = Vector2.up * jumpForce;
            bJump= false;
        }
    
    }

    #endregion


    public void AddWeapon(Weapons weaponInf)
    {

        this.weaponInfo = weaponInf;
        attack.SetWeaponDamage(weaponInfo.weaponDamage);
    
    }


    private void Attack()
    {
        //gets current game time and compare to attackcouter witch is the current time + time rate
        bool canAttack = Time.time > attackCounter;

        if (Input.GetButtonDown("Fire1") && weaponInfo != null && canAttack)
        {
           amim.SetTrigger("Attack_B");
           attack.PlayeAttackAnimationClip(weaponInfo.weaponAnimationClip);
           attackCounter = Time.time + attackRate;
        }
    }

}


