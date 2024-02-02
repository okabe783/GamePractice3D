using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackEffect : MonoBehaviour
{
    private Animator animator;

    //FireBall
    public GameObject fireBallPrefab;
    public GameObject clawAttackPrefab;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject muzzlePosition;
    [SerializeField] private GameObject clawAttackPosition;

    //Hp
    public int maxHp = 100;
    private int enemyHp;

    public Slider enemyHpBar;

    //死亡判定
    private bool isDeath;

    //script
    private EnemyNavMove enemyNavMove;

    private void Start()
    {
        enemyNavMove = GetComponent<EnemyNavMove>();
        animator = GetComponent<Animator>();
        //hpをMaxに
        enemyHp = maxHp;
        //HpBarUI
        if (enemyHpBar != null) enemyHpBar.value = enemyHp;
    }

    //Damage処理
    public void Damage(int damage)
    {
        if (enemyHp <= 0) return;
        //EnemyのHpを送られてきたint分減らしてHpBarに反映
        enemyHp -= damage;
        if (enemyHpBar != null) enemyHpBar.value = enemyHp;
        //Hpが0ならOnDieする
        if (enemyHp <= 0)
        {
            OnDie();
        }
    }

    //死亡処理
    void OnDie()
    {
        animator.SetTrigger("Death");
        enemyNavMove.ChangeDie();
    }

    void DragonHit()
    {
        //playerタグがついているオブジェクトを検索
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        //playerObjectが存在する場合
        if (playerObject != null)
        {
            //当たり判定用FireBallPrefabの生成
            GameObject nolmalAttack = Instantiate(fireBallPrefab, muzzlePosition.transform.position,
                fireBallPrefab.transform.rotation);
            //FireBallEffectの生成
            GameObject fireEffect = Instantiate(fireBall, muzzlePosition.transform.position,
                muzzlePosition.transform.rotation);
            Destroy(nolmalAttack, 1f);
            Destroy(fireEffect, 2f);
        }
    }

    void ClawAttack()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            GameObject clawAttack = Instantiate(clawAttackPrefab, clawAttackPosition.transform.position,
                clawAttackPosition.transform.rotation);
            Destroy(clawAttack, 1f);
            Debug.Log("a");
        }
    }
}