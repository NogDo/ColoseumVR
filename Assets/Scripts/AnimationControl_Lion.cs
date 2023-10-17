using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl_Lion : MonoBehaviour
{
    private Animator animator;
    public GameObject objLionHead;
    public LionAttack lionAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("EndAttack"))
            {
                Debug.Log("사자 공격 종료");
                lionAttack.EndAttack();
            }
        }
    }

    public void OnHit()
    {
        animator.SetTrigger("OnHit");
    }

    public void Dodge()
    {
        animator.SetTrigger("Dodge");
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    public void BiteAttack()
    {
        animator.SetTrigger("BiteAttack");
    }

    public void ClawsAttack()
    {
        animator.SetTrigger("ClawsAttack");
    }

    public void Walking()
    {
        animator.SetBool("Walk", true);
    }

    public void StopWalking()
    {
        animator.SetBool("Walk", false);
    }

    public void Running()
    {
        animator.SetBool("Run", true);
    }

    public void StopRunning()
    {
        animator.SetBool("Run", false);
    }
}
