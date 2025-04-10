using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace AE
{
    public class SwordPickup : MonoBehaviour, IInteractable
    {
        public OutlineObject outline;
        public AudioClip pickupSFX;

        private bool isInteractable = true;
        private AudioSource audioSource;

        void Start()
        {
            outline.enabled = false;
            audioSource = GetComponent<AudioSource>();
        }

        public async void Interact()
        {
            if (PlayerInventory.Instance.IsHoldingSword)
            {
                Debug.Log("Player is already holding sword");
                return;
            }

            if (isInteractable)
            {
                if (pickupSFX != null && audioSource != null)
                {
                    audioSource.clip = pickupSFX;
                    audioSource.loop = false;
                    audioSource.Play();

                    await UniTask.Delay((int)(pickupSFX.length * 500));
                }

                PlayerInventory.Instance.PickUpSword(this);
                gameObject.SetActive(false);
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }
        }
    }
}

