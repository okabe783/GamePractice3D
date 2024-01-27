using UnityEngine;

public class CharactorAttack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacked;
    public GameObject hitEffect;
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
                animator.SetBool("Attack", true);
            }
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
        animator.SetBool("Attack", false);
        animator.SetBool("Special", false);
        isAttacked = false;
        Debug.Log("消えました");
    }

    void AttackHit()
    {
        //エフェクトの生成
        GameObject effect = Instantiate(hitEffect,transform.position,Quaternion.identity) as GameObject;
        //位置を微調整
        effect.transform.localPosition = transform.position + new Vector3(0f, 0.5f, 0f);
        Destroy(effect,1f);
        Debug.Log("燃える");
        //Damage処理
    }
}