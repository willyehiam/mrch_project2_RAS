using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MRCH.Common.Tool
{
    public abstract class XROriginEditorControl : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, Unit(Units.Meter)] protected float moveSpeed = 2f;
        [SerializeField, Unit(Units.Meter)] protected float fastMoveSpeed = 6f;
        [SerializeField, Unit(Units.Meter)] protected float elevationSpeed = 2f;
        [SerializeField, Unit(Units.Degree)] protected float rotationSpeed = 15f;
        [SerializeField] protected float keyboardRotationMultiplier = 3f;

        protected Vector3 moveDirection = Vector3.zero;
        protected float currentSpeed;
        protected InputAction moveAction;
        protected InputAction elevateAction;
        protected InputAction shiftAction;
        protected InputAction rotateAction;
        protected InputAction mouseDeltaAction;
        protected InputAction rightClickAction;


        protected void Awake()
        {
            moveAction = new InputAction();
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            elevateAction = new InputAction();
            elevateAction.AddCompositeBinding("1DAxis")
                .With("Negative", "<Keyboard>/q")
                .With("Positive", "<Keyboard>/e");

            shiftAction = new InputAction("Shift", binding: "<Keyboard>/shift");

            rightClickAction = new InputAction("RightClick", binding: "<Mouse>/rightButton");

            rotateAction = new InputAction();
            rotateAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/i")
                .With("Down", "<Keyboard>/k")
                .With("Left", "<Keyboard>/j")
                .With("Right", "<Keyboard>/l");

            mouseDeltaAction = new InputAction("MouseDelta", binding: "<Mouse>/delta");
        }

        protected void OnEnable()
        {
            moveAction.Enable();
            elevateAction.Enable();
            shiftAction.Enable();
            rotateAction.Enable();
            rightClickAction.Enable();
            mouseDeltaAction.Enable();
        }

        protected void OnDisable()
        {
            moveAction.Disable();
            elevateAction.Disable();
            shiftAction.Disable();
            rotateAction.Disable();
            rightClickAction.Disable();
            mouseDeltaAction.Disable();
        }

        protected void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        protected void HandleMovement()
        {
            currentSpeed = shiftAction.ReadValue<float>() > 0 ? fastMoveSpeed : moveSpeed;

            var moveInput = moveAction.ReadValue<Vector2>();

            var elevationInput = elevateAction.ReadValue<float>();

            moveDirection = new Vector3(moveInput.x, elevationInput, moveInput.y);

            transform.Translate(moveDirection * (currentSpeed * Time.deltaTime), Space.Self);
        }

        protected void HandleRotation()
        {
            var isRotating = rightClickAction.ReadValue<float>() > 0;

            if (isRotating)
            {
                var mouseDelta = mouseDeltaAction.ReadValue<Vector2>();
                var yaw = mouseDelta.x * rotationSpeed * Time.deltaTime; // Yaw (horizontal rotation)
                var pitch = -mouseDelta.y * rotationSpeed * Time.deltaTime; // Pitch (vertical rotation)

                // Apply rotation to the XR Origin
                transform.Rotate(Vector3.up, yaw, Space.World);
                transform.Rotate(Vector3.right, pitch, Space.Self);
            }
            else
            {
                var rotateInput = rotateAction.ReadValue<Vector2>();

                var yaw = rotateInput.x * rotationSpeed * keyboardRotationMultiplier *
                          Time.deltaTime; // Yaw (horizontal rotation)
                var pitch = -rotateInput.y * rotationSpeed * keyboardRotationMultiplier *
                            Time.deltaTime; // Pitch (vertical rotation)

                transform.Rotate(Vector3.up, yaw, Space.World);
                transform.Rotate(Vector3.right, pitch, Space.Self);
            }
        }
#endif
    }
}