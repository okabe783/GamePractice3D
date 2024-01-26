using UnityEngine;

public class CharactorAttack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacked;
    public bool IsAttacked => isAttacked;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Attacking();
    }
    
    void Attacking()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isAttacked == false)
            {
                isAttacked = true;
                animator.SetBool("Attack",true);
            }
        }
    }
    public void AttackEnd()
    {
        animator.SetBool("Attack",false);
        isAttacked = false;
        Debug.Log("消えました");
    }
}
