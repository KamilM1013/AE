using UnityEngine;
using UnityEngine.UI;

namespace AE
{
    public class RaycastInteraction : MonoBehaviour
    {
        public float interactionDistance = 3f;
        public LayerMask interactableLayer;
        public GameObject interactPromptUI;
        public Camera playerCamera;

        private IInteractable currentInteractable = null;
        private OutlineObject outline = null;

        void Start()
        {
            if (interactPromptUI != null)
            {
                interactPromptUI.SetActive(false);
            }
        }

        void Update()
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, interactionDistance, interactableLayer))
            {
                IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
                outline = hitInfo.transform.GetComponent<OutlineObject>();

                if (interactable != null)
                {
                    currentInteractable = interactable;
                    if (interactPromptUI != null)
                    {
                        interactPromptUI.SetActive(true);
                    }
                    if (outline != null)
                    {
                        outline.enabled = true;
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        currentInteractable.Interact();
                    }
                }
                else
                {
                    currentInteractable = null;
                    if (interactPromptUI != null)
                    {
                        interactPromptUI.SetActive(false);
                    }
                    if (outline != null)
                    {
                        outline.enabled = false;
                    }
                }
            }
            else
            {
                currentInteractable = null;
                if (interactPromptUI != null)
                {
                    interactPromptUI.SetActive(false);
                }
                if (outline != null)
                {
                    outline.enabled = false;
                }
            }
        }
    }
}
