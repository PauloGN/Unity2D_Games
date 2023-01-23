using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rigidbodyREF;
    private bool isOnGround;
    private bool canDoubleJump;


    //Player properties
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashSpeed;

    //Controllers of time and counters     
    public float dashTime;
    private float dashCounter;
    private float dashImageTCounter;
    private float dashRechargeCounter;
    [SerializeField] private float waitAfterDashin;
    bool isBallMode;

    //Ground and Jump control
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask layerGround;

    //Animation Section
    [SerializeField] private Animator amim;
    [SerializeField] private Animator ballAmim;
    [SerializeField] public SpriteRenderer mainSpriteRender, dashFXSpriteRender;
    [SerializeField] private float dashSpriteLifeTime = 0.2f, timeBetweenSpriteImages;
    [SerializeField] private Color dashImageColor;
    [SerializeField] GameObject standing, ball;

    //Bullets and Shot
    [SerializeField] private BulletController bulletREF;
    [SerializeField] private Transform shotPoint;

    //Bomb section
    [SerializeField] private GameObject bombREF;


    // Start is called before the first frame update
    void Start()
    {
        
        Assert.IsNotNull(rigidbodyREF, "Rigidbody2D is null");

    }

    // Update is called once per frame
    void Update()
    {

        //holds functionalities for dash and move only dash in no ball mode
        bool bIsDashing = false;
        if (!isBallMode)
        {
           bIsDashing = Dash();
        }


        if (!bIsDashing)
        {
            CharacterMovement();
        }
        Jump();

        //Shot left moouse click
        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
        }

        //Turning modes
        TurnMode_Ball_Stading();

    }

    private void CharacterMovement()
    {
        
        float moveXSpeed = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rigidbodyREF.velocity = new Vector2( moveXSpeed ,rigidbodyREF.velocity.y);
        
        float currentVelocity = rigidbodyREF.velocity.x;
        //handle direction change
        if(currentVelocity < 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if(currentVelocity > 0)
        {
            transform.localScale = Vector3.one;
        }


        //Amination sprites for ball mode and standing mode
        if (isBallMode)
        {
            ballAmim.SetFloat("Speed_Param", Mathf.Abs(currentVelocity));
        }
        else if(!isBallMode)
        {
            //Move sideways
            amim.SetFloat("Speed_Param", Mathf.Abs(currentVelocity));
        }

    }

    private void Jump()
    {
        //checking if on the ground
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, layerGround);
        amim.SetBool("IsOnGround_Param", isOnGround);

        //Jumping
        if (Input.GetButtonDown("Jump") && (isOnGround || canDoubleJump))
        {
            //double jump
            if (isOnGround)
            {
                canDoubleJump = true;
            }
            else
            {
                canDoubleJump = false;
                amim.SetTrigger("DoubleJump_Param");
            }
            rigidbodyREF.velocity = new Vector2(rigidbodyREF.velocity.x, jumpForce);
        }

    }

    bool Dash()
    {

        //Control the frequency of calling dash ability
        if (dashRechargeCounter > 0)
        {
            dashRechargeCounter-=Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Fire2"))
            {
                dashCounter = dashTime;
                ShowDashImage();
            }
        }



        if (dashCounter > 0.0f)
        {
            rigidbodyREF.velocity = new Vector2(transform.localScale.x * dashSpeed, rigidbodyREF.velocity.y);
            dashCounter -= Time.deltaTime;
            dashImageTCounter -= Time.deltaTime;

            if(dashImageTCounter <= 0)
            {
                ShowDashImage();
            }

            //Can dash again
            dashRechargeCounter = waitAfterDashin;

            return true;
        }


        return false;
    }

    private void Shot()
    {

        if (isBallMode)
        {
            const float yOffset = .2f;
            Vector2 position = new Vector2(groundPoint.position.x, groundPoint.position.y + yOffset);
            var bulletInstance = Instantiate(bombREF, position, groundPoint.rotation);
        }
        else if(!isBallMode)
        {
            amim.SetTrigger("IsShooting_Param");
            var bulletInstance = Instantiate(bulletREF, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0.0f);
        }
    }


    private void ShowDashImage()
    {
        SpriteRenderer spriteRenderREF = Instantiate(dashFXSpriteRender, transform.position, transform.rotation);
        spriteRenderREF.sprite = mainSpriteRender.sprite;
        spriteRenderREF.transform.localScale = transform.localScale;
        spriteRenderREF.color = dashImageColor;

        Destroy(spriteRenderREF, dashSpriteLifeTime);

        dashImageTCounter = timeBetweenSpriteImages;
    }


    void TurnMode_Ball_Stading()
    {

        if (!isBallMode)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ball.SetActive(true);
                standing.SetActive(false);
            }
        }else if (isBallMode)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ball.SetActive(false);
                standing.SetActive(true);
            }
        }
        //checks if the ball sprite is active or not in the scene
        isBallMode = ball.activeSelf;
    }
}
