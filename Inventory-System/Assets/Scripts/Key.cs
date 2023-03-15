using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] ScriptableKey keyInfo;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to the renderer component and set the sprite info
        spriteRenderer= GetComponentInChildren<SpriteRenderer>(); 
        spriteRenderer.sprite = keyInfo.keySprite;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        PlayerController playerControllerREF = other.GetComponent<PlayerController>();
        if (playerControllerREF != null )
        {
          Inventory.Instance.AddKey(keyInfo);
          Destroy(gameObject);
        }
    }

}
