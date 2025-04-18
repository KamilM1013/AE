using UnityEngine;

namespace AE
{
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;

        [Header("Look Settings")]
        public float mouseSensitivity = 300f;
        public Camera playerCamera;

        public FootstepsAudio footstepsAudio;

        private CharacterController controller;
        private Vector3 verticalVelocity = Vector3.zero;
        private float xRotation = 0f;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            HandleMovement();
            HandleLook();
        }

        void HandleMovement()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 horizontalMove = transform.right * moveX + transform.forward * moveZ;
            Vector3 moveVelocity = horizontalMove * moveSpeed;

            if (controller.isGrounded && verticalVelocity.y < 0)
            {
                verticalVelocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            verticalVelocity.y += gravity * Time.deltaTime;

            moveVelocity.y = verticalVelocity.y;

            controller.Move(moveVelocity * Time.deltaTime);

            bool isMoving = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).sqrMagnitude > 0.5f;
            bool isWalking = isMoving && controller.isGrounded;

            if (footstepsAudio != null)
            {
                footstepsAudio.SetWalkingState(isWalking);
            }
        }

        void HandleLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 0.01f;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 0.01f;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            if (playerCamera != null)
            {
                playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }

            transform.Rotate(Vector3.up * mouseX);
        }
    }
}