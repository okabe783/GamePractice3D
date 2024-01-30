using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavMove : MonoBehaviour
{
    enum State
    {
        Walking, //探索
        Chasing,　//追跡
        Attacking,　//攻撃
        Died,　//死亡
    }

    private Animator animator;

    private NavMeshAgent navMeshAgent;

    //攻撃スパン
    private float delay = 3;

    private float timer = 0;

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
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            nextState = State.Chasing;
            navMeshAgent.destination = col.gameObject.transform.position;
            navMeshAgent.isStopped = false;
        }
    }

    public void OnLoseObject(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
            {
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
                    // エージェント(player)が進んでいる場合は、目標方向を向く
                    Vector3 direction = (navMeshAgent.destination - transform.position).normalized;
                    if (direction != Vector3.zero)
                    {
                        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                        transform.rotation = lookRotation;
                    }
                    else
                    {
                        Debug.LogWarning("Direction vector is zero. Using a default direction or alternative method.");
                    }
                }

                break;
            case State.Attacking:
                if (timer > delay)
                {
                    animator.SetTrigger("Attack");
                    timer = 0;
                }

                break;
            case State.Died:
                Destroy(this.gameObject, 5f);
                break;
        }
    }

    void DelayCount()
    {
        if (state != State.Attacking)
        {
            timer += Time.deltaTime;
        }
    }

    public void ChangeDie()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            //NavMeshが有効なとき、無効にして終了処理を行う
            navMeshAgent.enabled = false;
        }

        nextState = State.Died;
    }
}