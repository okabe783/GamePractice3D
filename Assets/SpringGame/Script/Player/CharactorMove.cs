using System.Collections;
using UnityEngine;

public class CharactorMove : MonoBehaviour
{
    //移動速度
    [SerializeField] float movePower = 3;
    [SerializeField] private GameObject warpEffect;

    Rigidbody rb;

    //キャラクターの移動方向を表すベクトル
    Vector3 dir;

    private Animator animator;
    private CharactorAttack charactorAttack;
    private WarpState warpState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        charactorAttack = GetComponent<CharactorAttack>();
    }

    void Update()
    {
        if (!charactorAttack.IsAttacked)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.A)) WarpA(WarpState.aa, 0, 10);
        if (Input.GetKeyDown(KeyCode.D)) WarpA(WarpState.dd, 0, -10);
        if (Input.GetKeyDown(KeyCode.W)) WarpA(WarpState.ww, 10, 0);
        if (Input.GetKeyDown(KeyCode.S)) WarpA(WarpState.ss, -10, 0);
    }

    void FixedUpdate()
    {
        Vector3 velo = rb.velocity;
        velo.y = 0;
        animator.SetFloat("Speed", velo.magnitude);
    }

    void Move()
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

    private void OnAnimatorMove()
    {
        transform.position = animator.rootPosition;
    }

    private bool warpCd = false;
    WarpState tmp = WarpState.kara;
    private void WarpA(WarpState warpState, int x, int z)
    {
        if (tmp == warpState)
        {
            if (warpCd == false)
            {
                var  newEffect= Instantiate(warpEffect, transform);
                Destroy(newEffect,1f);
                transform.position =
                    new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
                StartCoroutine(AaWarpCooldown());
            }
        }
        else
        {
            tmp = warpState;
        }
    }

    IEnumerator AaWarpCooldown()
    {
        warpCd = true;
        yield return new WaitForSeconds(2f);
        warpCd = false;
    }
}

enum WarpState
{
    aa,
    dd,
    ww,
    ss,
    kara
}