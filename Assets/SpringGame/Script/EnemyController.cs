using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CharacterController enemyController;

    private Animator animator;

    //目的地
    private Vector3 destination;

    [SerializeField] private float speed = 10f;

    //速度
    private Vector3 velocity;

    //移動方向
    private Vector3 direction;

    private bool arrived = false;

    private Vector3 startPosition;
    private void Start()
    {
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        var randomDesrination = Random.insideUnitCircle * 8;
        //ランダムに目的地を取得
        destination = startPosition + new Vector3(randomDesrination.x, 0, randomDesrination.y);
        //物体が動いていない状態
        velocity = Vector3.zero;
        startPosition = transform.position;
    }

    private void Update()
    {
        if (enemyController.isGrounded)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 2.0f);
            //目的地　- 現在のキャラの位置　=　方向ベクトル
            //normalizedは正規化した値を出す。最短方向を取り出す
            direction = (destination - transform.position).normalized;
            //引数にVector3をいれることで向かせたい位置を指定
            transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
            //歩くスピードをかけて移動させる
            velocity = direction * speed;
            Debug.Log(destination);
            //到着したのかどうかの判定
            //２地点の距離はDistanceで求めることができる
            if (Vector3.Distance(transform.position, destination) < 0.5f)
            {
                arrived = true;
                animator.SetFloat("Speed",0f);
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);
    }
}