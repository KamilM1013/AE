using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

namespace AE
{
    public class SkullSocket : MonoBehaviour, IInteractable
    {
        public Transform socketPoint;
        public OutlineObject outline;
        public AudioClip sheatheSFX;
        public AudioClip roarSFX;
        public AudioClip rattleSFX;
        public float insertAnimationTime = 0.5f;
        public float YOffset = 1.5f;

        public AudioSource swordAudioSource;
        public AudioSource roarAudioSource;

        private bool hasSword = false;

        void Start()
        {
            outline.enabled = false;
        }

        public void Interact()
        {
            if (hasSword)
            {
                Debug.Log("Skull already has a sword");
                return; 
            }

            var sword = PlayerInventory.Instance.heldSword;

            if (sword == null)
            {
                if (rattleSFX != null && swordAudioSource != null)
                {
                    swordAudioSource.clip = rattleSFX;
                    swordAudioSource.loop = false;
                    swordAudioSource.Play();
                }
                Debug.Log("Player is not holding sword");
                return;
            }

            if (sheatheSFX != null && swordAudioSource != null)
            {
                swordAudioSource.clip = sheatheSFX;
                swordAudioSource.loop = false;
                swordAudioSource.Play();
            }

            if (roarSFX != null && roarAudioSource != null)
            {
                roarAudioSource.clip = roarSFX;
                roarAudioSource.loop = false;
                roarAudioSource.Play();
            }

            Vector3 offsetStart = socketPoint.position + Vector3.up * YOffset;
            sword.transform.position = offsetStart;
            sword.transform.rotation = socketPoint.rotation;
            sword.gameObject.SetActive(true);

            sword.transform.DOMove(socketPoint.position, insertAnimationTime).SetEase(Ease.OutCubic);
            sword.transform.DORotateQuaternion(socketPoint.rotation, insertAnimationTime);

            hasSword = true;

            PlayerInventory.Instance.ClearSword();

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            PuzzleManager.Instance.RegisterSwordPlacement();

            Debug.Log("Sword placed in skull");
        }
    }
}

