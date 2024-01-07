using UnityEngine;

public class CharactorMove : MonoBehaviour
{
    [SerializeField] private float Speed;
    
    private CharacterController characterController;
    private Vector3 velocity; //速度
    private Animator animator;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //接地判定
        if (characterController.isGrounded)
        {
            //xは左右方向、yは上下なので0、zは前後方向を取得
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            //magnitudeはベクトルの長さ
            if (velocity.magnitude > 0.1f)
            {
                animator.SetFloat("Speed",velocity.magnitude);
                transform.LookAt(transform.position + velocity);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Speed * Time.deltaTime);
    }
}
