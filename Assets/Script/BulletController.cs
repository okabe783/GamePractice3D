using UnityEngine;

public class BulletController : MonoBehaviour
{
    //弾丸のオブジェクト
    public GameObject bullet;
    //弾丸の速度
    public float bulltSpeed;

    public void BulletShoot()
    {
        //弾丸が発射される向き
        GameObject Bullet = Instantiate(bullet, transform.position,
            Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
        Rigidbody bulletRb = Bullet.GetComponent<Rigidbody>();
        //弾丸の速度
        bulletRb.AddForce(transform.forward * -bulltSpeed);
        //弾丸が消える時間
        Destroy(Bullet,3.0f);
    }
}