using UnityEngine;
using UnityEngine.InputSystem;

//必須コンポーネント
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Charactor : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    public float jumpSpeed = 10;
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

    public void Move(Vector2 input)
    {
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

    bool CheckForGround()
    {
        //Charactorのコライダーから下まで(中心から下向きベクトルを高さの半分だけかける)の計算
        Vector3 capsuleBottom = capsuleCollider.center + Vector3.down * capsuleCollider.height * 0.5f;
        //charactorの足元の位置を計算。カプセルの下をワールド座標に変換して上向きのベクトルを地面の距離だけかける
        Vector3 feetPosition = transform.TransformPoint(capsuleBottom)　+ Vector3.up * groundDistance;

        //Raycastは足元よりも少し上の位置から始める必要がある
        bool raycastHit = Physics.Raycast(feetPosition, Vector3.down, groundDistance * 2f,groundMask);
        
        return raycastHit;
    }

    // fixedUpdateはjumpしている場合、重力と反比例の力を加える。なのでrigidbodyの重量を忘れずに計算。  
    private void FixedUpdate()
    {
        isGround = CheckForGround();
        if (rb.velocity.y < 0)
        {
            isJumping = false;
        }
        //jumpの高さを調節
        if (isJumping)
        {
            rb.AddForce(Physics.gravity * rb.mass * (jumpGravityScale - 1f));
        }
    }
}