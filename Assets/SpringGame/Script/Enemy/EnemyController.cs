using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Walk,
        Wait,
        Chase
    };

    private CharacterController enemyController;
    private Animator animator;
    private SetPosition setPosition;

    [SerializeField] private float speed; //歩くスピード
    [SerializeField] private float waitTime = 2.0f; //待ち時間


    private Vector3 destination; //目的地
    private Vector3 velocity; //速度
    private Vector3 dir; //移動方向

    private Vector3 basePosition;
    private float elapsedTime; //残り時間
    private float walkRange = 5.0f;
    private bool arrived = false; //到着フラグ

    private EnemyState state;
    private Transform playerTransform;

    private void Start()
    {
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        setPosition = GetComponent<SetPosition>();
        setPosition.CreateRandomPosition(); //ランダムに目的地を取得
        velocity = Vector3.zero; //物体が動いていない状態
        elapsedTime = 0f;
        Setstate(EnemyState.Walk);
    }

    private void Update()
    {
        tracking();
    }


    public void tracking()
    {
        //見回りまたはキャラクターを追いかける状態
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == EnemyState.Chase)
            {
                setPosition.SetDestination(playerTransform.position);
            }

            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                //目的地　- 現在のキャラの位置　=　方向ベクトル
                //normalizedは正規化した値を出す。最短方向を取り出す
                dir = (setPosition.GetDestination() - transform.position).normalized;
                //引数にVector3をいれることで向かせたい位置を指定
                transform.LookAt(new Vector3(setPosition.GetDestination().x,
                    transform.position.y, setPosition.GetDestination().z));
                velocity = dir * speed; //歩くスピードをかけて移動させる
            }

            Debug.Log(setPosition.GetDestination());

            //到着したのかどうかの判定
            //２地点の距離はDistanceで求めることができる
            if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 1.9f)
            {
                Setstate(EnemyState.Wait);
                animator.SetFloat("Speed", 0.0f);
            }
        }
        //到着していたら一定時間まつvelocity
        else if (state == EnemyState.Wait)
        {
            elapsedTime += Time.deltaTime;
            //待ち時間を超えていたら次の目的地を再設定
            if (elapsedTime > waitTime)
            {
                Setstate(EnemyState.Walk);
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity);
    }

    //敵キャラクターの状態変更
    public void Setstate(EnemyState tempState, Transform targetObj = null)
    {
        if (tempState == EnemyState.Walk)
        {
            arrived = false;
            elapsedTime = 0f;
            state = tempState;
            setPosition.CreateRandomPosition();
        }
        else if (tempState == EnemyState.Chase)
        {
            state = tempState;
            //待機状態から追いかける場合もあるのでOff
            arrived = false;
            //追いかける対象をセット
            playerTransform = targetObj;
        }
        else if (tempState == EnemyState.Wait)
        {
            elapsedTime = 0f;
            state = tempState;
            arrived = true;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }
    }

    //敵キャラクターの状態取得
    public EnemyState GetState()
    {
        return state;
    }
}