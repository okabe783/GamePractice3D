using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAnimation : MonoBehaviour
{
    private Animator animator;
    private CharactorStatus status;
    private bool isDown, attacked;

    public bool IsAttacked()
    {
        return attacked;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        status = GetComponent<CharactorStatus>();
    }
    
    void Update()
    {
        if (attacked && !status.attacking)
        {
            attacked = false;
        }
        animator.SetBool("Attack",!attacked && status.attacking);

        if (!isDown && status.died)
        {
            isDown = true;
            animator.SetTrigger("Down");
        }
    }
}
