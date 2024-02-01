using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyAttackEffect : MonoBehaviour
{
    private Animator animator;

    //FireBall
    public GameObject fireBallPrefab;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject muzzlePosition;

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
            //当たり判定用FireBallPrefabの生成
            GameObject nolmalAttack = Instantiate(fireBallPrefab, muzzlePosition.transform.position,fireBallPrefab.transform.rotation);
            //FireBallEffectの生成
            GameObject fireEffect = Instantiate(fireBall, muzzlePosition.transform.position, muzzlePosition.transform.rotation);
            Destroy(nolmalAttack, 1f);
            Destroy(fireEffect, 2f);
        }
    }
}
//GameObject nolmalAttack = Instantiate(fireBallCol, playerPosition + new Vector3(1f, 0.5f, 0f),
//    Quaternion.identity);