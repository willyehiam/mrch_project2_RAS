using Sirenix.OdinInspector;
using UnityEngine;

namespace MRCH.Common.Interact
{
    public abstract class KeepFacingToCam : MonoBehaviour
    {
        protected Camera _mainCam;

        protected bool _faceToCam = false;

        [Title("Setting")] [SerializeField] protected bool lockYAxis = false;
        [SerializeField] protected bool faceToCamOnEnable = true;

        protected virtual void Start()
        {
            _mainCam = Camera.main;

            if (GetComponent(typeof(MoveAndRotate)) != null)
            {
                Debug.LogWarning($"{gameObject.name} has both 'TextFaceToCam' and 'Move and Rotate' component!");
            }

            _faceToCam = faceToCamOnEnable;
        }

        protected virtual void Update()
        {
            if (!_mainCam || !_faceToCam) return;

            var directionToCamera = _mainCam.transform.position - transform.position;
            if (lockYAxis)
                directionToCamera.y = 0;
            transform.rotation = Quaternion.LookRotation(-directionToCamera);
        }

        public virtual void SetFaceToCam(bool target)
        {
            _faceToCam = target;
        }
    }
}