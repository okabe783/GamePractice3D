using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] [Tooltip("追いかける対象")] private GameObject player;

    private NavMeshAgent navMeshAgent;
    void Start()
    {
        //NavMeshAgentを保持しておく
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //プレイヤーを目指して進む
        navMeshAgent.destination = player.transform.position;
    }
}
