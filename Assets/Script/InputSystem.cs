using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class InputSystem : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    [SerializeField] private BulletController bulletController;
    private float speed = 3.0f;
    //x軸方向の入力を保存
    private float inputX;

    //z軸方向の入力を保存
    private float inputZ;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //現在のマウス情報
        var mouseCurrent = Mouse.current;
        //マウスの接続チェック
        if (mouseCurrent == null)
        {
            //マウスが接続されていないとMouse.currentがnullになる
            return;
        }

        //左ボタンが押された瞬間かどうか
        if (mouseCurrent.leftButton.wasPressedThisFrame)
        {
            animator.SetBool("GunShoot", true);
            bulletController.BulletShoot();
        }

        //左ボタンが離された瞬間かどうか
        if (mouseCurrent.leftButton.wasReleasedThisFrame)
        {
            Invoke("BoolSet", 0.5f);
        }
        //入力を受け付ける
        inputX　= Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        //移動の向きなど座標関連はVector3で扱う
        Vector3 dir = new Vector3(inputX, 0, inputZ);
        //入力された方向を「カメラを基準としたXZ平面上のベクトル」に変換する
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        //キャラクターを「入力された方向」に向ける
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }
        //Y軸方向の速度を保ちながら、速度ベクトルを求めてセットする
        Vector3 velocity = dir.normalized * speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    void BoolSet()
    {
        animator.SetBool(("GunShoot"), false);
    }
}