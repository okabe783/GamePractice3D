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

    private CharactorStatus status;
    private Animator animator;
    private State state = State.Walking; //現在のステート
    private State nextState = State.Walking;
    private NavMeshAgent navMeshAgent;
    private float delay = 3;
    [SerializeField]
    private float timer = 0;

    private void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        status = GetComponent<CharactorStatus>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (state != nextState)
        {
            state = nextState;
        }
        
        switch (state)
        {
            case State.Walking:
                //経路探索が終了していれば
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
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
                {
                    animator.SetFloat("Speed", 0.0f);
                    nextState = State.Attacking;
                }
                break;
            case State.Attacking:
                AttackStart();
                break;
            case State.Died:
                Destroy(this.gameObject,5f);
                break;
        }

        DelayCount();
    }

    public void OnDetect(Collider col)
    {
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            animator.SetFloat("Speed", 0.5f);
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
                    nextState = State.Walking;
                    animator.SetFloat("Speed", 0f);
                    navMeshAgent.isStopped = true;
                }
            }
        }
    }
    void AttackStart()
    {
        if (timer > delay)
        {
            animator.SetTrigger("Attack");
            timer = 0;
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
// public void OnDetect(Collider col)
// {
//     //設定した対象を追いかける
//     if (col.gameObject.CompareTag("Player"))
//     {
//         animator.SetFloat("Speed", 0.5f);
//         nextState = State.Chasing;
//         navMeshAgent.destination = col.gameObject.transform.position;
//         navMeshAgent.isStopped = false;
//     }
// }
//
// public void OnLoseObject(Collider col)
// {
//     if (col.gameObject.CompareTag("Player"))
//     {
//         //目的地を今の自分の場所にして止まる
//         if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
//         {
//             nextState = State.Walking;
//             animator.SetFloat("Speed", 0f);
//             navMeshAgent.isStopped = true;
//         }
//     }
// }