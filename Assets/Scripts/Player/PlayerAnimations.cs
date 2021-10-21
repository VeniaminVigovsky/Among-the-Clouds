using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    string currentAnimation;
    float length;
    float time;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    public void ChangeAnimation(string animation)
    {
        if (animation == "Spin")
        {
            length = animator.GetCurrentAnimatorStateInfo(0).length;
            
        }


        if (currentAnimation == "Spin" && animation == "Fall")
        {
            time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (time >= length)
            {
                currentAnimation = animation;
                animator.Play(animation);
            }

            else
            {
                StopAllCoroutines();
                StartCoroutine(ChangeFromSpin(animation));
            }
        }

        else
        {
            currentAnimation = animation;
            animator.Play(animation);
        }

    }

    IEnumerator ChangeFromSpin(string animation)
    {
        yield return new WaitForSeconds(length - time);
        currentAnimation = animation;
        animator.Play(animation);

    }

}
