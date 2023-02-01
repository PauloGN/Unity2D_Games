using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [HideInInspector]
    public static PlayerHealthController Instance;

    [SerializeField] int maxHealth;
    private int health;


    private void Awake()
    {
        Instance= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(maxHealth <=0)
        {
            maxHealth = 1;
        }

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DamagePlayer(int dmg)
    {
        health-=dmg;
        if(health <=0)
        {
            health = 0;
            gameObject.SetActive(false);
        }

    }


}
