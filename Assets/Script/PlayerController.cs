using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float 		Accel		= 0.025f;
    public float 		MaxSpeed	= 2.0f;
    public float 		Handling 	= 50.0f;
    public float 		JumpForce 	= 10.0f;

    private float 		_brake = 0.1f;
    private bool		_isJump = false;
    private Rigidbody 	_rigidBody;
    private Animator	_animator;

    private float _accell = 0;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _brake = MaxSpeed / 10.0f;
    }
    void Update()
    {
        Move();
        Rotation();
        Jump();
    }

    void Move()
    {
        //前後移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("押した");
            _accell += Accel;
            if (_accell > 1.0f)
            {
                _accell = 1.0f;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                _accell -= Accel;
                if (_accell < -1.0f)
                {
                    _accell = 1.0f;
                }
                else
                {
                    if (_accell > 0)
                    {
                        _accell -= _brake;
                    }

                    if (_accell < 0)
                    {
                        _accell += _brake;
                    }

                    if (Mathf.Abs(_accell) <= _brake)
                    {
                        _accell = 0f;
                    }

                    float speedMatrix = 1.0f;
                    float speed = _accell * MaxSpeed * speedMatrix;
                    transform.position += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;

                    if (_animator)
                    {
                        //アニメーション速度
                        if (_accell > 0)
                        {
                            _animator.speed = 0.5f + _accell * 0.5f;
                        }
                        else
                        {
                            _animator.speed = 1.0f;
                        }
                        //Animatorのパラメーターに値をセット
                        _animator.SetFloat("Move",Mathf.Abs(speed));
                    }
                }
            }
        }
    }
    private void Rotation()
    {
        // 回転
        Vector3 rot = transform.rotation.eulerAngles;
        float handle = 0f;
        if (Input.GetKey (KeyCode.RightArrow))
        {
            handle = Handling * Time.deltaTime;
            rot.y += handle;
        }
        if (Input.GetKey (KeyCode.LeftArrow))
        {
            handle = Handling * Time.deltaTime;
            rot.y -= handle;
        }
        transform.rotation = Quaternion.Euler (rot);
    }
    private void Jump()
    {
        // ジャンプ
        if (Input.GetKey (KeyCode.Space))
        {
            if (!_isJump)
            {
                _rigidBody.AddForce (Vector3.up * JumpForce, ForceMode.Impulse);
            }
            _isJump = true;
        }
        else
        {
            _isJump = false;
        }
    }
}