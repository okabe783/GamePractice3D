using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    Animator animator;
    [SerializeField] private BulletController _bulletController;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //現在のマウス情報
        var current = Mouse.current;

        //キーボードの接続チェック
        if (current == null)
        {
            //マウスが接続されていないとMouse.currentがnullになる
            return;
        }

        //マウスカーソル位置取得
        var cursorPosition = current.position.ReadValue();
        //左ボタンが押されたかどうか
        var leftButton = current.leftButton;
        //左ボタンが押された瞬間かどうか
        if (leftButton.wasPressedThisFrame)
        {
            animator.SetBool("GunShoot",true);
            _bulletController.BulletShoot();
        }
        //左ボタンが離された瞬間かどうか
        if (leftButton.wasReleasedThisFrame)
        {
            Debug.Log($"左ボタンが離された{cursorPosition}");
            Invoke("BoolSet",0.5f);
        }
        // //左ボタンを押しているかどうか
        // if (leftButton.isPressed)
        // {
        //     Debug.Log($"左ボタンを押している{cursorPosition}");
        // }
    }
    void BoolSet()
    {
        animator.SetBool(("GunShoot"),false);
    }
}