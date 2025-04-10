using UnityEngine;
using DG.Tweening;
using System;

namespace AE
{
    public class Candleholder : MonoBehaviour, IInteractable
    {
        public Vector3 uprightRotation = new Vector3(0f, 0f, 0f);
        public float uprightPosition = 0f;
        public float uprightAnimationTime = 3.0f;

        public GameObject flameEffect;
        public AudioClip crackleSFX;
        public AudioClip scrapeSFX;
        public OutlineObject outline;

        public bool isUpright = false;

        private AudioSource audioSource;
        private bool isLit = false;

        void Start()
        {
            flameEffect.SetActive(false);
            outline.enabled = false;
            audioSource = GetComponent<AudioSource>();
        }

        public void Interact()
        {
            if (!isUpright)
            {
                if (scrapeSFX != null && audioSource != null)
                {
                    audioSource.clip = scrapeSFX;
                    audioSource.loop = false;
                    audioSource.Play();
                }

                Sequence sequence = DOTween.Sequence();
                sequence.Join(transform.DORotate(uprightRotation, uprightAnimationTime));
                sequence.Join(transform.DOMoveY(uprightPosition, 0.5f));
                sequence.Play()
                    .OnComplete(() => 
                    {
                        isUpright = true;
                        Debug.Log("Candleholder is now upright");
                    });
            }
            else if (!isLit)
            {
                if (flameEffect != null)
                {
                    flameEffect.SetActive(true);
                }

                if (crackleSFX != null && audioSource != null)
                {
                    audioSource.clip = crackleSFX;
                    audioSource.loop = true;
                    audioSource.Play();
                }

                isLit = true;
                PuzzleManager.Instance.RegisterCandlesLit();
                Debug.Log("Candleholder is now lit");
            }
            else
            {
                Debug.Log("Candleholder is already lit");
            }
        }
    }
}
