using UnityEngine;

public class EnemyAttackSkill : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(TryGetComponent<CharactorStatus>(out var  ima));

        if (col.TryGetComponent<CharactorStatus>(out var iDamage))
        {
            iDamage.AtDamage(10);
            Debug.Log(col);
        }
    }
}
