using UnityEngine;
using UnityEngine.Audio;

namespace AE
{
    public class FootstepsAudio : MonoBehaviour
    {
        public Animator animator;

        private AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void SetWalkingState(bool isWalking)
        {
            if (animator != null)
            {
                animator.SetBool("IsWalking", isWalking);
            }
        }

        void PlayFootstepSound(AudioClip audio)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(audio);
        }
    }
}
