using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //Atributes
    private int damage;

    //References
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    //Attack animation clip function
    public void PlayeAttackAnimationClip(AnimationClip clip)
    {
        animator.Play(clip.name);
    }

    public void SetWeaponDamage(int dmg)
    {
       damage = dmg;
    }


}
