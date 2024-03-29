using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private AudioClip kickSound;
    [SerializeField] private AudioClip SpecialEffect;
    [SerializeField] private AudioClip bless;
    [SerializeField] private AudioClip enemyStep;

    //Playerの足音
    public void PlayFootStepSound()
    {
        audioSource.PlayOneShot(footstepSound);
    }
    //通常攻撃
    public void PlayKickSound()
    {
        audioSource.PlayOneShot(kickSound);
    }
    //必殺技
    public void PlaySpecialSound()
    {
        audioSource.PlayOneShot(SpecialEffect);
    }
    //EnemyのBless攻撃
    public void EnemyBlessSound()
    {
        audioSource.PlayOneShot(bless);
    }

    public void EnemyStep()
    {
        audioSource.PlayOneShot(enemyStep);
    }
}