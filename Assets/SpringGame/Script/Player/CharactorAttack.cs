using UnityEngine;

public class CharactorAttack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacked;
    public GameObject specialEffect;
    public GameObject kickEffect;
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
                isAttacked = true;
                animator.SetTrigger("Attack");
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isAttacked == false)
            {
                isAttacked = true;
                animator.SetBool("Special", true);
            }
        }
    }

    void AttackEnd()
    {
        //animator.SetTrigger("Attack");
        animator.SetBool("Special", false);
        isAttacked = false;
        Debug.Log("消えました");
    }

    void AttackHit()
    {
        //エフェクトの生成
        GameObject effect = Instantiate(specialEffect,transform.position,Quaternion.identity);
        //位置を微調整
        effect.transform.localPosition = transform.position + new Vector3(0f, 0.5f, 0f);
        Destroy(effect,1f);
    }

    void KickEffect()
    {
        GameObject kick = Instantiate(kickEffect, transform.position, Quaternion.identity);
        kick.transform.localPosition = transform.position + new Vector3(0f, 0.5f, 0f);
        Destroy(kick,0.5f);
    }
}