using UnityEngine;

public class EnemyAttackSkill : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<CharactorStatus>(out var iDamage))
        {
            iDamage.AtDamage(10);
        }
    }
}
