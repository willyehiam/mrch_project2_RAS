using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace MRCH.Common.Interact
{
    public abstract class MoveAndRotate : MonoBehaviour
    {
        [Title("Move Options"), Required] public Transform moveTarget;
        [Unit(Units.MetersPerSecond)] public float moveSpeed = 2f;
        [HideIf("moveForthAndBackOnEnable")] public bool moveForOnceOnEnable = false;
        [HideIf("moveForOnceOnEnable")] public bool moveForthAndBackOnEnable = false;

        [Space, SerializeField] protected Ease moveType = Ease.InOutSine;

        [Title("Rotate Options")] public bool keepRotatingOnEnable = false;
        public Vector3 rotationAxis = Vector3.up;

        [Unit(Units.Second)] public float rotateDuration = 10f;

        [Space, SerializeField] protected Ease rotateType = Ease.Linear;

        protected Vector3 _initialPosition;

        protected Tween _moveTween;
        protected Tween _rotateTween;

        protected virtual void Awake()
        {
            _initialPosition = transform.position;
        }

        protected virtual void OnEnable()
        {
            if (moveForthAndBackOnEnable)
            {
                StopPlayingTween(_moveTween);
                MoveForthAndBack();
            }
            else if (moveForOnceOnEnable)
            {
                StopPlayingTween(_moveTween);
                MoveForOnce();
            }

            if (keepRotatingOnEnable)
            {
                StopPlayingTween(_rotateTween);
                RotateObject();
            }
        }

        public virtual void MoveForOnce()
        {
            if (!moveTarget)
            {
                Debug.LogError("Move target not set on MoveAndRotate" + gameObject.name);
                return;
            }

            StopPlayingTween(_moveTween);
            _moveTween = transform.DOMove(moveTarget.position, moveSpeed);
        }

        public virtual void MoveBackForOnce()
        {
            StopPlayingTween(_moveTween);
            _moveTween = transform.DOMove(_initialPosition, moveSpeed);
        }

        public virtual void JumpBackToInitialPosition()
        {
            StopPlayingTween(_moveTween);
            transform.position = _initialPosition;
        }

        public virtual void MoveForthAndBack()
        {
            if (!moveTarget)
            {
                Debug.LogError("Move target not set on MoveAndRotate" + gameObject.name);
                return;
            }

            StopPlayingTween(_moveTween);
            _moveTween = transform.DOMove(moveTarget.position, moveSpeed)
                .SetEase(moveType)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public virtual void RotateObject()
        {
            StopPlayingTween(_rotateTween);
            _rotateTween = transform.DORotate(rotationAxis * 360, rotateDuration, RotateMode.FastBeyond360)
                .SetEase(rotateType)
                .SetLoops(-1, LoopType.Restart);
        }

        protected virtual void StopPlayingTween(Tween tween)
        {
            if (tween == null || !tween.IsActive()) return;
            if (tween.IsPlaying())
                tween.Kill();
        }

        public virtual void StopMovement()
        {
            _moveTween.Kill();
        }

        public virtual void StopRotation()
        {
            _rotateTween.Kill();
        }

        protected virtual void OnDisable()
        {
            // Kill all tweens on this GameObject when disabled
            transform.DOKill();
            //moveTarget.DOKill();
        }
    }
}