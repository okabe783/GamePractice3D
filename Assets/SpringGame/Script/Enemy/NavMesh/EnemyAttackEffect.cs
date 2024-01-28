using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyAttackEffect : MonoBehaviour
{
    public GameObject dragonNolmal;
    private Animator animator;
    public int maxHp = 100;
    private int hp;
    private bool isDeath;
    private bool moveEnabled = true;
    public Slider hpBar;
    public UnityEvent onDieCallback = new UnityEvent();
    private EnemyNavMove enemyNavMove;

    private void Start()
    {
        enemyNavMove = GetComponent<EnemyNavMove>();
        hp = maxHp;
        animator = GetComponent<Animator>();
        if (hpBar != null)
        {
            hpBar.value = hp;
        }
    }

    public void Damage(int damage)
    {
        if (hp <= 0)
        {
            return;
        }

        hp -= damage;
        if (hpBar != null)
        {
            hpBar.value = hp;
        }

        if (hp <= 0)
        {
            OnDie();
        }
    }

    void OnDie()
    {
        animator.SetTrigger("Death");
        onDieCallback.Invoke();
        enemyNavMove.ChangeDie();
    }

    void DragonHit()
    {
        //エフェクトの生成
        GameObject nolmalAttack = Instantiate(dragonNolmal,transform.position,Quaternion.identity);
        //位置を微調整←ここ変更
        nolmalAttack.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
        Destroy(nolmalAttack,1f);
    }
}
