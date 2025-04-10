using UnityEngine;
using DG.Tweening;

namespace AE
{
    public class SkullSocket : MonoBehaviour, IInteractable
    {
        public Transform socketPoint;
        public float insertAnimationTime = 0.5f;
        public float YOffset = 1.5f;

        private bool hasSword = false;

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
                Debug.Log("Player is not holding sword");
                return;
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

