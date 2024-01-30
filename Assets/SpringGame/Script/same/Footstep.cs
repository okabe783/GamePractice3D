using UnityEngine;

public class Footstep : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip footstepSound;

    public void PlayFootStepSound()
    {
        audioSource.PlayOneShot(footstepSound);
    }
}
