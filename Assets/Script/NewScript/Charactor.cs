using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//必須コンポーネント
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Charactor : MonoBehaviour
{
    private Animator animator;
    public Camera playerCamera = null;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private Rigidbody groundRigidbody = null; //地面のrb
    private Vector3 groundNormal = Vector3.up; //地面の法線
    private Vector3 groundContactPoint = Vector3.zero; //地面と接触している点の座標

    public float jumpSpeed = 10;
    public float runSpeed = 3f;
    public float acceleration = 10f;
    public float maxGroundAngle = 45;
    public float jumpGravityScale = 0.6f;
    public float groundDistance = 0.01f;

    private bool isGround = false;

    private bool isJumping = false;

    //地面として認識しないコライダーの設定
    public LayerMask groundMask = ~0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private Vector2 movementInput = Vector2.zero;

    public void Move(Vector2 input) //ここでは移動させていない。FixedUpdateで動きを更新している
    {
        movementInput = input;
    }

    void OnMove(InputValue inputValue)
    {
        Move(inputValue.Get<Vector2>());
    }

    public void Jump(bool state)
    {
        if (state & isGround)
        {
            rb.velocity += Vector3.up * jumpSpeed;
            isJumping = true;
        }

        if (!state)
        {
            isJumping = false;
        }
    }

    void OnJump(InputValue inputValue)
    {
        Jump(inputValue.isPressed);
    }

    //RaycastHitはunityが衝突について纏めたクラス。
    RaycastHit CheckForGround()
    {
        float extent = Mathf.Max(0, capsuleCollider.height * 0.5f - capsuleCollider.radius);
        Vector3 origin = transform.TransformPoint(capsuleCollider.center + Vector3.down * extent) +
                         Vector3.up * groundDistance;

        RaycastHit hitInfo;
        Ray sphereCastRay = new Ray(origin, Vector3.down);
        Physics.SphereCast(sphereCastRay, capsuleCollider.radius, out hitInfo, groundDistance * 2f, groundMask);
        return hitInfo;
    }

    void ApplyMotion()
    {
        //右と前のデフォルトを設定
        Vector3 movementRight = Vector3.right;
        Vector3 movementForward = Vector3.forward;

        if (playerCamera != null)
        {
            //カメラに対して前と右の方向を取得
            Vector3 cameraRight = playerCamera.transform.right;
            Vector3 cameraForward = playerCamera.transform.forward;
            
            //地面に対する方向を計算する
            movementRight = ProjectOnPlane(cameraRight, groundNormal).normalized;
            movementForward = ProjectOnPlane(cameraForward, groundNormal).normalized;
        }
        
        //player入力と地面の右と前を組み合わせる
        Vector3 movement = movementRight * movementInput.x + movementForward * movementInput.y;
        //加速度を設定する
        rb.AddForce(movement * acceleration,ForceMode.Acceleration);
        //rbに力を加えて加速。forcemodeで最初の引数が絶対値の加速度を表していることを示している。
        rb.AddForce(new Vector3(movementInput.x, 0, movementInput.y) * acceleration, ForceMode.Acceleration);
        Vector3 rotate = new Vector3(movement.x, 0, movement.z);
        if (rotate.magnitude > 0.1f)
        {
            transform.LookAt(transform.position + rotate);
        }
    }

    // fixedUpdateはjumpしている場合、重力と反比例の力を加える。なのでrigidbodyの重量を忘れずに計算。  
    private void FixedUpdate()
    {
        RaycastHit hitInfo = CheckForGround();
        //衝突していない場合raycasthitクラスのコライダーがnull
        isGround = hitInfo.collider != null;
        if (isGround)
        {
            groundNormal = hitInfo.normal;
            groundContactPoint = hitInfo.point;
            groundRigidbody = hitInfo.rigidbody;
        }
        else
        {
            //地面にたっていないのでデフォルトにする
            groundNormal = Vector3.up;
            groundRigidbody = null;
            //地面と接触している点をカプセルコライダーの一番下の点にする
            groundContactPoint =
                transform.TransformPoint(capsuleCollider.center + Vector3.down * capsuleCollider.height * 0.5f);
        }
        if (rb.velocity.y < 0)
        {
            isJumping = false;
        }

        //jumpの高さを調節
        if (isJumping)
        {
            rb.AddForce(Physics.gravity * rb.mass * (jumpGravityScale - 1f));
        }

        //地面に立ってジャンプを実行したとき呼ばれる
        if (!isJumping && isGround)
        {
            ApplyMotion();
        }
    }

    Vector3 ProjectOnPlane(Vector3 vector, Vector3 normal) //外積は非可換
    {
        return Vector3.Cross(normal,Vector3.Cross(vector,normal));
    }
}