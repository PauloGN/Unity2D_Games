using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{

    enum EUA_UnlockAbility
    {
        EUA_DoubleJump,
        EUA_Dash,
        EUA_Ball,
        EUA_Bomb,
        EUA_None
    }


    [SerializeField] EUA_UnlockAbility unlockAbility;
    [SerializeField] GameObject pickupFX;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerAbilityTracker playerAbilityTrackerREF = other.GetComponentInParent<PlayerAbilityTracker>();
            if (playerAbilityTrackerREF != null)
            {
                switch (unlockAbility)
                {
                    case EUA_UnlockAbility.EUA_DoubleJump:
                        {
                            playerAbilityTrackerREF.bCanDoubleJump = true;
                            break;
                        }
                    case EUA_UnlockAbility.EUA_Dash:
                        {
                            playerAbilityTrackerREF.bCanDash = true;
                            break;
                        }
                    case EUA_UnlockAbility.EUA_Ball:
                        {
                            playerAbilityTrackerREF.bCanBecomeBall = true;
                            break;
                        }
                    case EUA_UnlockAbility.EUA_Bomb:
                        {
                            playerAbilityTrackerREF.bCanDropBombs = true;
                            break;
                        }
                    default:
                        {

                            Debug.Log("A Type must be set here...");

                            break;
                        }
                }

                if (pickupFX)
                {
                    Instantiate(pickupFX, transform.position, transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }


}
