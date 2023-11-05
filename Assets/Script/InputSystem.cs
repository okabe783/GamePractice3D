using UnityEngine;
using UnityEngine.InputSystem;
public class InputSystem : MonoBehaviour
{
    Animator animator;
    [SerializeField] private BulletController bulletController;
    private void Start()
    {
        animator = GetComponent<Animator>();
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
            animator.SetBool("GunShoot",true);
            bulletController.BulletShoot();
        }
        //左ボタンが離された瞬間かどうか
        if (mouseCurrent.leftButton.wasReleasedThisFrame)
        {
            Invoke("BoolSet",0.5f);
        }
    }
    void BoolSet()
    {
        animator.SetBool(("GunShoot"),false);
    }
}