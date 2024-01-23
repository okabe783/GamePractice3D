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
    
    private State state = State.Walking; //現在のステート
    private State nextState = State.Walking;
    private NavMeshAgent navMeshAgent;
    private void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (state != nextState)
        {
            OnStateExit();
            state = nextState;
            OnStateEnter();
        }

        switch (state)
        {
            case State.Walking:
                // if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
                // {
                //     nextState = State.Walking;
                // }
                break;
            case State.Chasing:
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
                {
                    nextState = State.Walking;
                }
                break;
            case State.Attacking:
                break;
            case State.Died:
                break;
        }
    }

    public void OnStateEnter()
    {
        switch (state)
        {
            case State.Walking:
                break;
            case State.Chasing:
                break;
            case State.Attacking:
                break;
            case State.Died:
                break;
        }
        Debug.Log(state);
    }

    void OnStateExit()
    {
        switch (state)
        {
            case State.Walking:
                break;
            case State.Chasing:
                break;
            case State.Attacking:
                break;
            case State.Died:
                break;
        }
    }
    public void OnDetect(Collider col)
    {
        //設定した対象を追いかける
        if (col.gameObject.CompareTag("Player"))
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
            //目的地を今の自分の場所にして止まる
            //navMeshAgent.destination = this.gameObject.transform.position;
            nextState = State.Walking;
            navMeshAgent.isStopped = true;
        }
    }
    
}
