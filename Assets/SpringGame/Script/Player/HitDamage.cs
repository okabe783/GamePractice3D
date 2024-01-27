using UnityEngine;

public class HitDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var status = other.GetComponent<EnemyAttackEffect>();
            status.Damage(10);
        }
    }
}
