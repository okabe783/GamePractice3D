using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavMove : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    public void OnDetect(Collider col)
    {
        //設定した対象を追いかける
        if (col.gameObject.CompareTag("Player"))
        {
            navMeshAgent.destination = col.gameObject.transform.position;
        }
    }

    public void OnLoseObject(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //目的地を今の自分の場所にして止まる
            navMeshAgent.destination = this.gameObject.transform.position;
        }
    }
}
