using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 3.0f;

    //x軸方向の入力を保存
    private float inputX;

    //z軸方向の入力を保存
    private float inputZ;

    void Update()
    {
        //x軸方向、z軸方向の入力を取得
        //Horizontal、水平、横方向のイメージ
        inputX　= Input.GetAxis("Horizontal");
        //Vertical、垂直、縦方向のイメージ
        inputZ = Input.GetAxis("Vertical");

        //移動の向きなど座標関連はVector3で扱う
        Vector3 velocity = new Vector3(inputX, 0, inputZ);
        //ベクトルの向きを取得
        Vector3 direction = velocity.normalized;

        //移動距離を計算
        float distance = speed * Time.deltaTime;
        //移動先を計算
        Vector3 destination = transform.position + direction * distance;

        //移動先に向けて回転
        transform.LookAt(destination);
        //移動先の座標を設定
        transform.position = destination;
    }
}