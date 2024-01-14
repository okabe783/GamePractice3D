using System;
using UnityEngine;

public class SearthArea : MonoBehaviour
{
    private EnemyController moveEnemy;

    private void Start()
    {
        moveEnemy = GetComponent<EnemyController>();
    }
    private void OnTriggerStay(Collider col)　//接触しているのかどうかを検知する
    {
        if (col.tag == "Player")
        {
            //敵キャラクターの状態を取得
            EnemyController.EnemyState state = moveEnemy.GetState();
            //敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if(state != EnemyController.EnemyState.Chase)
            {
                moveEnemy.Setstate(EnemyController.EnemyState.Chase, col.transform);
            }
        }
    }

    private void OnTriggerExit(Collider col) //オブジェクトが接触状態から抜けた時に呼ばれる
    {
        if (col.tag == "Player")
        {
            moveEnemy.Setstate(EnemyController.EnemyState.Wait);
        }
    }
}
