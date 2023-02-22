using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObj : MonoBehaviour
{

    //references
    [SerializeField] Weapons weaponInfo;
    private SpriteRenderer spriteR;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        spriteR.sprite = weaponInfo.weaponSprite;

        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        PlayerController playerREF= GetComponent<PlayerController>();

        if (playerREF != null)
        {
            playerREF.AddWeapon(weaponInfo);
        }
    }

}
