using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    [SerializeField] float movingSpeed = 5f;

    /// <summary>ターンの速さ</summary>
    [SerializeField] float turnSpeed = 3f;
    public Animator animator;
    Rigidbody rb;

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

    //入力の受付
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
            //bulletController.BulletShoot();
            //animator.SetTrigger("Shot");
        }

        //左ボタンが離された瞬間かどうか
        if (mouseCurrent.leftButton.wasReleasedThisFrame)
        {
            
        }
    }

    //30フレームに固定
    //実行間隔が変わったら困る処理
    private void FixedUpdate()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 入力方向のベクトルを組み立てる
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        else
        {
            // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
            dir = Camera.main.transform.TransformDirection(dir); // メインカメラを基準に入力方向のベクトルを変換する
            dir.y = 0; // y 軸方向はゼロにして水平方向のベクトルにする

            // 入力方向に滑らかに回転させる
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation =
                Quaternion.Slerp(this.transform.rotation, targetRotation,
                    Time.deltaTime * turnSpeed); // Slerp を使うのがポイント

            Vector3 velo = dir.normalized * movingSpeed; // 入力した方向に移動する
            rb.velocity = velo; // 計算した速度ベクトルをセットする
        }
    }

    //updateの後に呼ばれる処理
    void LateUpdate()
    {
        // アニメーションを操作する
        Vector3 velocity = rb.velocity;
        velocity.y = 0; // 上下方向の速度は無視する
        animator.SetFloat("Move", velocity.magnitude);
    }
}