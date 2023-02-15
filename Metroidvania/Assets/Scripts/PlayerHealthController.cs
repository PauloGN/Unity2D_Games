using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerHealthController : MonoBehaviour
{
    [HideInInspector]
    public static PlayerHealthController instance;

    [SerializeField] int maxHealth;
    [SerializeField] float invencibilityLength;
    [SerializeField] float flashLength;
    [SerializeField] SpriteRenderer [] playerSpritesREF;

    private int health;
    private float invencibilityCounter;
    private float flashCounter;


    private void Awake()
    {
        if (instance == null)
        {           
            instance= this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(maxHealth <=0)
        {
            maxHealth = 1;
        }

        health = maxHealth;

        UIController.instance.UpdateHealth(health, maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
        if(invencibilityCounter > 0)
        {
            invencibilityCounter -=Time.deltaTime;
            flashCounter-= Time.deltaTime;

            if (flashCounter <= 0)
            {
                flashCounter = flashLength;
                foreach(var sprite in playerSpritesREF)
                {
                    sprite.enabled= !sprite.enabled;
                }
            }


            if(invencibilityCounter <= 0)
            {
                foreach (var sprite in playerSpritesREF)
                {
                    sprite.enabled = true;
                }
                flashCounter= 0;
            }
        }


    }


    public void DamagePlayer(int dmg)
    {

        if(invencibilityCounter > 0)
        {
            return;
        }

        health-=dmg;

        if(health <=0)
        {
            health = 0;
            gameObject.SetActive(false);

            RespawnController.instance.Respawn();

        }else
        {
            invencibilityCounter=invencibilityLength;
        }



        UIController.instance.UpdateHealth(health, maxHealth);
    }

    public void RestorePlayerState()
    {
        health = maxHealth;
        UIController.instance.UpdateHealth(health, maxHealth);
    }

}
