using System;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject col)
    {
        var status = GetComponent<CharactorStatus>();
        status.Damage(20);
    }
}