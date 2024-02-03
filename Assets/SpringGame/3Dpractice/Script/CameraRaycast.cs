using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public Camera mainCamera; //メインカメラの参照
    public Transform targetObject; //空のオブジェクトのTransform
    public Animator animator; //アニメーターコンポーネント
    
    void Update()
    {
        //Aimがtrueの時のみ処理を実行
        if (animator.GetBool("Aim"))
        {
            //カメラの位置から画面中央に向かってレイを飛ばす
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2,0));
            //レイの原点から方向に10m伸ばした座標を計算して空のオブジェクトに代入
            targetObject.position = ray.origin + ray.direction * 10f;
        }
    }
}
