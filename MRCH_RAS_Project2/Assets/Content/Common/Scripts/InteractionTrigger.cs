using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace MRCH.Common.Interact
{
    /// <summary>
    /// author: Shengyang Billiton Peng
    /// 
    /// Enables three kinds of triggers to invoke events: collider, distance, and look at.
    /// DO NOT CHANGE THE SCRIPT! COPY THE CODE TO YOUR FOLDER AND CHANGE THE CLASS NAME IF YOU WANT TO MODIFY IT.
    /// You can inherit this class and override specific methods.
    /// </summary>
    public abstract class InteractionTrigger : MonoBehaviour
    {
        #region Variables

        #region Position

        [Title("Collider Trigger"), SerializeField]
        private bool useColliderTrigger;


        private Collider _colliderTrigger;

        [Space, ShowIf("useColliderTrigger"), SerializeField, Indent]
        private UnityEvent onTriggerFirstEnter;

        [ShowIf("useColliderTrigger"), SerializeField, Indent]
        private UnityEvent onTriggerEnter;

        [ShowIf("useColliderTrigger"), SerializeField, Indent]
        private UnityEvent onTriggerExit;

        private bool _firstColliderEnter = true;

        #endregion

        #region Distance

        [Title("Distance Trigger"), Space, SerializeField]
        private bool useDistanceTrigger;

        [ShowIf("useDistanceTrigger"), SerializeField, Indent, Unit(Units.Meter)]
        protected float distance = 10f;

        [Space, ShowIf("useDistanceTrigger"), SerializeField, Indent]
        private UnityEvent onDistanceFirstEnter;

        [ShowIf("useDistanceTrigger"), SerializeField, Indent]
        private UnityEvent onDistanceEnter;

        [ShowIf("useDistanceTrigger"), SerializeField, Indent]
        private UnityEvent onDistanceExit;

        private bool _firstDistanceEnter = true;
        private bool _alreadyInDistance;

        #endregion

        #region Lookat

        [Title("LookAt Trigger"), Space, SerializeField]
        private bool useLookAtTrigger;

        [ShowIf("useLookAtTrigger"), SerializeField, Indent, Unit(Units.Degree)]
        protected float lookAtAngle = 25f;

        [ShowIf("useLookAtTrigger"), SerializeField, Indent, Unit(Units.Meter)]
        protected float lookAtDistance;

        [Space, ShowIf("useLookAtTrigger"), SerializeField, Indent]
        private UnityEvent onLookAtFirstEnter;

        [ShowIf("useLookAtTrigger"), SerializeField, Indent]
        private UnityEvent onLookAtEnter;

        [ShowIf("useLookAtTrigger"), SerializeField, Indent]
        private UnityEvent onLookAtDistanceExit;

        private bool _firstLookAtEnter = true;
        private bool _alreadyLookAt;

        #endregion

        #region "unity events"

        [Title("Events Triggers"), Space, SerializeField]
        private bool useEventsTriggers;

        [Space, ShowIf("useEventsTriggers"), SerializeField, Indent]
        private bool useStartTrigger;

        [ShowIf("@this.useEventsTriggers && this.useStartTrigger"), SerializeField, Indent(2)]
        private UnityEvent onStart;

        [ShowIf("useEventsTriggers"), SerializeField, Indent]
        private bool useOnEnableTrigger;

        [ShowIf("@this.useEventsTriggers && this.useOnEnableTrigger"), SerializeField, Indent(2)]
        private UnityEvent onEnable;

        [ShowIf("useEventsTriggers"), SerializeField, Indent]
        private bool useUpdateTrigger;

        [ShowIf("@this.useEventsTriggers && this.useUpdateTrigger"), SerializeField, Indent(2),
         InfoBox("WAIT, ARE YOU SURE YOU NEED THIS??", InfoMessageType.Warning, "useUpdateTrigger")]
        private UnityEvent onUpdate;

        [ShowIf("useEventsTriggers"), SerializeField, Indent]
        private bool useOnDisableTrigger;

        [ShowIf("@this.useEventsTriggers && this.useOnDisableTrigger"), SerializeField, Indent(2)]
        private UnityEvent onDisable;

        #endregion

        #region Global Variables

        private GameObject _player;
        private Transform _playerTransform;
        protected const int CheckRateFreq = 25;

        #endregion

        #region Setting

        [Space, Title("Setting", bold: false), SerializeField]
        protected bool debugMode = false;

        #endregion

        #endregion

        protected virtual void Start()
        {
            CheckAndInitSetting();

            _player = GameObject.FindGameObjectWithTag("Player");
            if (_player == null)
                Debug.LogError("No player found in the scene");
            _playerTransform = _player.transform;

            if (useEventsTriggers && useStartTrigger)
                TriggerOnStart();
        }

        protected virtual void OnEnable()
        {
            if (useEventsTriggers && useOnEnableTrigger)
                TriggerOnEnable();
        }

        protected virtual void Update()
        {
            if (useDistanceTrigger)
            {
                if (!CheckRateLimiter(CheckRateFreq)) return;

                if (InDistance(distance) && !_alreadyInDistance)
                {
                    if (_firstDistanceEnter)
                    {
                        TriggerOnDistanceFirstEnter();
                        _firstDistanceEnter = false;
                    }

                    TriggerOnDistanceEnter();
                    _alreadyInDistance = true;
                }
                else if (!InDistance(distance) && _alreadyInDistance)
                {
                    TriggerOnDistanceExit();
                    _alreadyInDistance = false;
                }

                if (useEventsTriggers && useUpdateTrigger)
                    TriggerOnUpdate();
            }

            if (useLookAtTrigger)
            {
                if (!CheckRateLimiter(CheckRateFreq)) return;
                if (InDistance(lookAtDistance) && !_alreadyLookAt)
                {
                    if (Vector3.Angle(_playerTransform.forward,
                            (transform.position - _playerTransform.position).normalized) <= lookAtAngle)
                    {
                        if (_firstLookAtEnter)
                        {
                            TriggerOnLookAtFirstEnter();
                            _firstLookAtEnter = false;
                        }

                        TriggerOnLookAtEnter();
                        _alreadyLookAt = true;
                    }
                }
                else if (!InDistance(lookAtDistance) && _alreadyLookAt)
                {
                    TriggerOnLookAtExit();
                    _alreadyLookAt = false;
                }
            }
        }

        protected virtual void OnDisable()
        {
            if (useEventsTriggers && useOnDisableTrigger)
                TriggerOnDisable();
        }

        protected bool InDistance(float dist)
        {
            return Vector3.Distance(transform.position, _playerTransform.position) <= dist;
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (!useColliderTrigger) return;
            if (!(other.CompareTag("Player") || other.CompareTag("MainCamera"))) return;

            if (_firstColliderEnter)
            {
                TriggerOnTriggerFirstEnter();
                _firstColliderEnter = false;
            }

            TriggerOnTriggerEnter();
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!useColliderTrigger) return;
            if (!other.CompareTag("Player")) return;

            TriggerOnTriggerExit();
        }

        public virtual void OnTriggerStay(Collider other)
        {
        }

        protected void CheckAndInitSetting()
        {
            if (useColliderTrigger)
            {
                _colliderTrigger = GetComponent<Collider>();
                if (_colliderTrigger == null)
                    Debug.LogError("Collider Trigger is enabled but no collider is attached to " + gameObject.name);
                else if (_colliderTrigger.isTrigger == false)
                    Debug.LogWarning("YOU SHOULD PROBABLY TURN ON 'isTrigger' of the collider on " + gameObject.name);

                if (onTriggerFirstEnter == null && onTriggerEnter == null && onTriggerExit == null)
                    Debug.LogWarning("No events are assigned to Collider Trigger on " + gameObject.name);
            }

            if (useDistanceTrigger)
            {
                if (distance == 0)
                    Debug.LogWarning("Distance Trigger is enabled but distance is set to 0 on " + gameObject.name);

                if (onDistanceFirstEnter == null && onDistanceEnter == null && onDistanceExit == null)
                    Debug.LogWarning("No events are assigned to Distance Trigger on " + gameObject.name);
            }

            if (useLookAtTrigger)
            {
                if (onLookAtFirstEnter == null && onLookAtFirstEnter == null && onLookAtDistanceExit == null)
                    Debug.LogWarning("No events are assigned to LookAt Trigger on " + gameObject.name);
            }

            if (useEventsTriggers)
            {
                if (useStartTrigger && onStart == null)
                    Debug.LogWarning("No events are assigned to Start Trigger on " + gameObject.name);
                if (useOnEnableTrigger && onEnable == null)
                    Debug.LogWarning("No events are assigned to OnEnable Trigger on " + gameObject.name);
                if (useUpdateTrigger && onUpdate == null)
                    Debug.LogWarning("No events are assigned to Update Trigger on " + gameObject.name);
                if (useOnDisableTrigger && onDisable == null)
                    Debug.LogWarning("No events are assigned to OnDisable Trigger on " + gameObject.name);
            }
        }

        public static bool CheckRateLimiter(float frequency)
        {
            return Time.frameCount % frequency == 0;
        }

        #region TriggerEachEvents

        public virtual void TriggerOnTriggerFirstEnter()
        {
            if (debugMode)
                if (debugMode)
                    Debug.Log("onTriggerFirstEnter is triggered on " + gameObject.name);
            onTriggerFirstEnter?.Invoke();
        }

        public virtual void TriggerOnTriggerEnter()
        {
            if (debugMode)
                Debug.Log("onTriggerEnter is triggered on " + gameObject.name);
            onTriggerEnter?.Invoke();
        }

        public virtual void TriggerOnTriggerExit()
        {
            if (debugMode)
                Debug.Log("onTriggerExit is triggered on " + gameObject.name);
            onTriggerExit?.Invoke();
        }

        public virtual void TriggerOnDistanceFirstEnter()
        {
            if (debugMode)
                Debug.Log("onDistanceFirstEnter is triggered on " + gameObject.name);
            onDistanceFirstEnter?.Invoke();
        }

        public virtual void TriggerOnDistanceEnter()
        {
            if (debugMode)
                Debug.Log("onDistanceEnter is triggered on " + gameObject.name);
            onDistanceEnter?.Invoke();
        }

        public virtual void TriggerOnDistanceExit()
        {
            if (debugMode)
                Debug.Log("onDistanceExit is triggered on " + gameObject.name);
            onDistanceExit?.Invoke();
        }

        public virtual void TriggerOnLookAtFirstEnter()
        {
            if (debugMode)
                Debug.Log("onLookAtFirstEnter is triggered on " + gameObject.name);
            onLookAtFirstEnter?.Invoke();
        }

        public virtual void TriggerOnLookAtEnter()
        {
            if (debugMode)
                Debug.Log("onLookAtEnter is triggered on " + gameObject.name);
            onLookAtEnter?.Invoke();
        }

        public virtual void TriggerOnLookAtExit()
        {
            if (debugMode)
                Debug.Log("onLookAtExit is triggered on " + gameObject.name);
            onLookAtDistanceExit?.Invoke();
        }

        public virtual void TriggerOnStart()
        {
            if (debugMode)
                Debug.Log("onStart is triggered on " + gameObject.name);
            onStart?.Invoke();
        }

        public virtual void TriggerOnEnable()
        {
            if (debugMode)
                Debug.Log("onEnable is triggered on " + gameObject.name);
            onEnable?.Invoke();
        }

        public virtual void TriggerOnUpdate()
        {
            if (debugMode)
                Debug.Log("onEnable is triggered on " + gameObject.name);
            onUpdate?.Invoke();
        }

        public virtual void TriggerOnDisable()
        {
            if (debugMode)
                Debug.Log("onDisable is triggered on " + gameObject.name);
            onDisable?.Invoke();
        }

        #endregion
    }
}