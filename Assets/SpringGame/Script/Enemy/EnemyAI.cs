using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Wait,
        Move,
        Attack,
        MoveAndAttack,
        Idle,
        Avoid
    };
}
