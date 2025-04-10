using UnityEngine;

namespace AE
{
    public class SwordPickup : MonoBehaviour, IInteractable
    {
        private bool isInteractable = true;

        public void Interact()
        {
            if (PlayerInventory.Instance.IsHoldingSword)
            {
                Debug.Log("Player is already holding sword");
                return;
            }

            if (isInteractable)
            {
                PlayerInventory.Instance.PickUpSword(this);
                gameObject.SetActive(false);
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }
        }
    }
}

