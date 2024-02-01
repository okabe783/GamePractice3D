using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackEffect : MonoBehaviour
{
    private Animator animator;

    //FireBall
    public GameObject fireBallCol;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject muzzle;

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
        Vector3 playerPosition = playerObject.transform.position;

        //playerObjectが存在する場合
        if (playerObject != null)
        {
            //エフェクトの生成
            GameObject nolmalAttack = Instantiate(fireBallCol, playerPosition + new Vector3(1f, 0.5f, 0f),
                Quaternion.identity);
            //FireBallEffectの生成
            GameObject fireEffect = Instantiate(fireBall, muzzle.transform.position - new Vector3(0f, 0f, 3f),
                Quaternion.identity);
            Destroy(nolmalAttack, 1f);
            Destroy(fireEffect, 3f);
        }
    }
}