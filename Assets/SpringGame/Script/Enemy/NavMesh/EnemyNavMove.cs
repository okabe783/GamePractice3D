using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavMove : MonoBehaviour
{
    private enum State
    {
        Walking, //探索
        Chasing,　//追跡
        Attacking,　//攻撃
        Died,　//死亡
    }

    private Animator animator;

    private NavMeshAgent navMeshAgent;

    //攻撃スパン
    private float delay = 2;
    private float timer;

    //Stateの初期値
    private State state = State.Walking;
    private State nextState = State.Walking;
    public float rotationSpeed = 2f;

    private void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (state != nextState)
        {
            state = nextState;
        }

        EnemyState();
        DelayCount();
    }

    //追跡開始
    public void OnDetect(Collider col)
    {
        //NavMeshがアクティブでnullじゃなければ
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            nextState = State.Chasing;
            navMeshAgent.destination = col.gameObject.transform.position;
            navMeshAgent.isStopped = false;
        }
    }

    //見失ったとき
    public void OnLoseObject(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
            {
                //距離を比較
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
                {
                    nextState = State.Attacking;
                    animator.SetFloat("Speed", 0f);
                    navMeshAgent.isStopped = true;
                }
            }
        }
    }

    void EnemyState()
    {
        switch (state)
        {
            case State.Walking:
                //経路探索が終了していればAnimを終了
                if (!navMeshAgent.pathPending)
                {
                    animator.SetFloat("Speed", 0.0f);
                    nextState = State.Walking;
                }
                else
                {
                    nextState = State.Chasing;
                }

                break;
            case State.Chasing:
                //remainingDistance→目標地点まで進んだ距離=>stoppingDistance→目標地点に到達する前に停止する距離　＝ 目的地にほぼ到達したかどうか
                //navMeshAgent.pathPending→新しい経路を計算中かどうか
                animator.SetFloat("Speed", 0.5f);
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
                {
                    animator.SetFloat("Speed", 0.0f);
                    nextState = State.Attacking;
                    // エージェント(player)が進んでいる場合は、目標方向に回転
                    Vector3 direction = (navMeshAgent.destination - transform.position).normalized;
                    if (direction != Vector3.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
                            rotationSpeed * Time.deltaTime);
                    }
                }

                break;
            case State.Attacking:
                if (timer > delay)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        Attack();
                    }
                    else
                    {
                        ClawAttack();
                    }

                    timer = 0;
                }

                break;
            case State.Died:
                Destroy(this.gameObject, 5f);
                break;
        }
    }

    //攻撃Speed
    void DelayCount()
    {
        if (state != State.Attacking)
        {
            timer += Time.deltaTime;
        }
    }

    //死亡Stateに変更するメソッド
    public void ChangeDie()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            //NavMeshが有効なとき、無効にして終了処理を行う
            navMeshAgent.enabled = false;
        }

        nextState = State.Died;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void ClawAttack()
    {
        animator.SetTrigger("ClawAttack");
    }
}