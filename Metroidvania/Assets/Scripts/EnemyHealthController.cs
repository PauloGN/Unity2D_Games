using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    [SerializeField] int totalHealth = 3;
    [SerializeField] GameObject deathFX;
    [SerializeField] SpriteRenderer spriteRendererREF;
    [SerializeField] Color[] colors;
    private bool bHasDetachedObj = false;

    private int colorsIndex;


    private void Start()
    {
        colorsIndex = colors.Length -1;
        bHasDetachedObj = GetComponent<EnemyPatroller>();
    }


    public void TakeDamage(int dmg)
    {
        totalHealth-= dmg;
        ChangeEnemyColor();

        if (totalHealth <=0)
        {
            if(deathFX != null)
            {
                Instantiate(deathFX, transform.position, transform.rotation);
            }

            if (bHasDetachedObj)
            {
                //destroy deatached points if needed
                SendMessage("DestroyDetached");
            }
            Destroy(gameObject);
        }
    }

    void ChangeEnemyColor()
    {
        spriteRendererREF = GetComponentInChildren<SpriteRenderer>();
        if (spriteRendererREF != null && colors.Length >= colorsIndex)
        {

            if(totalHealth > 0 && totalHealth < colors.Length)
            {
                colorsIndex--;
                spriteRendererREF.color = colors[colorsIndex];
            }
        }
    }

}
