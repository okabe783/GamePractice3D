using UnityEngine;

public class EnemyAttackEffect : MonoBehaviour
{
    public GameObject dragonNolmal;
    void DragonHit()
    {
        //エフェクトの生成
        GameObject nolmalAttack = Instantiate(dragonNolmal,transform.position,Quaternion.identity);
        //位置を微調整←ここ変更
        nolmalAttack.transform.localPosition = transform.position + new Vector3(0f, 0.5f, 0f);
        Destroy(nolmalAttack,1f);
    }
}
