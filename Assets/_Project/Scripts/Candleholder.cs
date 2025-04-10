using UnityEngine;
using DG.Tweening;

namespace AE
{
    public class Candleholder : MonoBehaviour, IInteractable
    {
        public Vector3 uprightRotation = new Vector3(0f, 0f, 0f);
        public float uprightPosition = 0f;
        public float uprightAnimationTime = 3.0f;

        public GameObject flameEffect;

        public bool isUpright = false;

        private bool isLit = false;

        public void Interact()
        {
            if (!isUpright)
            {
                transform.DORotate(uprightRotation, uprightAnimationTime)
                    .OnComplete(() => 
                    {
                        isUpright = true;
                        Debug.Log("Candleholder is now upright");
                    });
                transform.DOMoveY(uprightPosition, 0.5f);
            }
            else if (!isLit)
            {
                if (flameEffect != null)
                {
                    flameEffect.SetActive(true);
                } 
                isLit = true;
                Debug.Log("Candleholder is now lit");
            }
            else
            {
                Debug.Log("Candleholder is already lit");
            }
        }
    }
}
