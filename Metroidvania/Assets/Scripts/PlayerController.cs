using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
    public float dashSpeed;
    public float dashTime;
    private float dashCounter;

    //Ground and Jump control
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask layerGround;

    //Animation Section
    [SerializeField] private Animator amim;
    [SerializeField] public SpriteRenderer mainSpriteRender, dashFXSpriteRender;
    [SerializeField] private float dashSpriteLifeTime = 0.2f, timeBetweenSpriteImages;
    private float dashImageTCounter;
    [SerializeField] private Color dashImageColor;

    //Bullets and Shot
    [SerializeField] private BulletController bulletREF;
    [SerializeField] private Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        
        Assert.IsNotNull(rigidbodyREF, "Rigidbody2D is null");



    }

    // Update is called once per frame
    void Update()
    {
        bool bIsDashing = Dash();

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

        //Move sideways
        amim.SetFloat("Speed_Param", Mathf.Abs(currentVelocity));

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

        if (Input.GetButtonDown("Fire2"))
        {
            dashCounter = dashTime;
            ShowDashImage();
        }

        if(dashCounter > 0.0f)
        {
            rigidbodyREF.velocity = new Vector2(transform.localScale.x * dashSpeed, rigidbodyREF.velocity.y);
            dashCounter -= Time.deltaTime;
            dashImageTCounter -= Time.deltaTime;

            if(dashImageTCounter <= 0)
            {
                ShowDashImage();
            }

            return true;
        }


        return false;
    }

    private void Shot()
    {
        amim.SetTrigger("IsShooting_Param");
        var bulletInstance = Instantiate(bulletREF, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0.0f);
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


}
