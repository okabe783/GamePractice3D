using UnityEngine;

public class Footstep : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private AudioClip kickSound;
    [SerializeField] private AudioClip SpecialEffect;

    //足音
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
}