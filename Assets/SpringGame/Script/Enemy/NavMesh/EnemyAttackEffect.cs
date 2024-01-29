using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyAttackEffect : MonoBehaviour
{
    public GameObject dragonNolmal;
    private Animator animator;
    public int maxHp = 100;
    private int enemyHp;
    private bool isDeath;
    private bool moveEnabled = true;
    public Slider enemyHpBar;
    public UnityEvent onDieCallback = new UnityEvent();
    private EnemyNavMove enemyNavMove;

    private void Start()
    {
        enemyNavMove = GetComponent<EnemyNavMove>();
        enemyHp = maxHp;
        animator = GetComponent<Animator>();
        if (enemyHpBar != null)
        {
            enemyHpBar.value = enemyHp;
        }
    }

    public void Damage(int damage)
    {
        if (enemyHp <= 0)
        {
            return;
        }

        enemyHp -= damage;
        if (enemyHpBar != null)
        {
            enemyHpBar.value = enemyHp;
        }

        if (enemyHp <= 0)
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
        //playerタグがついているオブジェクトを検索
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        //playerObjectが存在する場合
        if (playerObject != null)
        {
            //playerの座標を取得
            Vector3 playerPosition = playerObject.transform.position;
            //エフェクトの生成
            GameObject nolmalAttack = Instantiate(dragonNolmal, playerPosition, Quaternion.identity);
            //位置を微調整
            nolmalAttack.transform.position = playerPosition + new Vector3(0f, 0.5f, 0f);
            Destroy(nolmalAttack, 1f);
        }
    }
}