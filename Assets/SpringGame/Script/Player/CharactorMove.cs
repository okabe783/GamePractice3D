using UnityEngine;

public class CharactorMove : MonoBehaviour
{
    //移動速度
    [SerializeField] float movePower = 3;
    Rigidbody rb;
    //キャラクターの移動方向を表すベクトル
    Vector3 dir;

    private Animator animator;
    private bool attacked;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //水平方向と垂直方向の移動を取得
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //入力方向を計算しカメラの向きを合わせる
        dir = Vector3.forward * v + Vector3.right * h;
        dir = Camera.main.transform.TransformDirection(dir);
        //上下方向の移動を無効
        dir.y = 0;
        //移動方向を正規化
        Vector3 forward = dir.normalized;
        rb.velocity = forward * movePower;
        forward.y = 0;
        //キャラクターの向きを移動方向に合わせる
        if (forward != Vector3.zero)
        {
            this.transform.forward = forward;
        }
    }

    void FixedUpdate()
    {
        Vector3 velo = rb.velocity;
        velo.y = 0;
        animator.SetFloat("Speed", velo.magnitude);
    }
}