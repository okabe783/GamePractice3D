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
    public float groundDistance = 0.01f;
    private bool isGround = false;

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
            isGround = true;
        }
    }

    void OnJump(InputValue inputValue)
    {
        Jump(inputValue.isPressed);
    }

    bool CheckForGround()
    {
        //capsuleBottomはオブジェクトに対して相対的な足元の位置からfeetPosition(絶対的な位置)に変換
        Vector3 capsuleBottom = capsuleCollider.center + Vector3.down * capsuleCollider.height * 0.5f;
        Vector3 feetPosition = transform.TransformPoint(capsuleBottom)　+ Vector3.up * groundDistance;

        //Raycastは足元よりも少し上の位置から始める必要がある
        bool raycastHit = Physics.Raycast(feetPosition, Vector3.down, groundDistance * 2f);
        
        return raycastHit;
    }

    private void FixedUpdate()
    {
        isGround = CheckForGround();
    }
}